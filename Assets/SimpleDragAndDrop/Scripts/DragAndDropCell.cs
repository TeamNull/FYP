using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Every item's cell must contain this script
/// </summary>
[RequireComponent(typeof(Image))]
public class DragAndDropCell : MonoBehaviour, IDropHandler
{
    public enum CellType                                                    // Cell types
    {
        Swap,                                                               // Items will be swapped between any cells
        DragOnly                                                            // Item will be dragged from this cell
    }

    public enum TriggerType                                                 // Types of drag and drop events
    {
        DropRequest,                                                        // Request for item drop from one cell to another
        DropEventEnd,                                                       // Drop event completed
        ItemAdded,                                                          // Item manualy added into cell
        ItemWillBeDestroyed                                                 // Called just before item will be destroyed
    }

    public class DropEventDescriptor                                        // Info about item's drop event
    {
        public TriggerType triggerType;                                     // Type of drag and drop trigger
        public DragAndDropCell sourceCell;                                  // From this cell item was dragged
        public DragAndDropCell destinationCell;                             // Into this cell item was dropped
        public DragAndDropItem item;                                        // Dropped item
        public bool permission;                                             // Decision need to be made on request
    }

    public CellType cellType = CellType.Swap;                               // Special type of this cell
    public bool unlimitedSource;                                    // Item from this cell will be cloned on drag start

    private DragAndDropItem myDadItem;										// Item of this DaD cell

    void OnEnable()
    {
        DragAndDropItem.OnItemDragStartEvent += OnAnyItemDragStart;         // Handle any item drag start
        DragAndDropItem.OnItemDragEndEvent += OnAnyItemDragEnd;             // Handle any item drag end
        UpdateMyItem();
    }

    void OnDisable()
    {
        DragAndDropItem.OnItemDragStartEvent -= OnAnyItemDragStart;
        DragAndDropItem.OnItemDragEndEvent -= OnAnyItemDragEnd;
        StopAllCoroutines();                                                // Stop all coroutines if there is any
    }

    /// <summary>
    /// On any item drag start need to disable all items raycast for correct drop operation
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragStart(DragAndDropItem item)
    {
        UpdateMyItem();
        if (myDadItem != null)
        {
            myDadItem.MakeRaycast(false);                                  	// Disable item's raycast for correct drop handling
        }
    }

    /// <summary>
    /// On any item drag end enable all items raycast
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragEnd(DragAndDropItem item)
    {
        UpdateMyItem();
        if (myDadItem != null)
        {
            myDadItem.MakeRaycast(true);                                  	// Enable item's raycast
        }
    }

    /// <summary>
    /// Item is dropped in this cell
    /// </summary>
    /// <param name="data"></param>
    public void OnDrop(PointerEventData data)
    {
        if (DragAndDropItem.icon != null)
        {
            DragAndDropItem item = DragAndDropItem.draggedItem;
            DragAndDropCell sourceCell = DragAndDropItem.sourceCell;
            if (DragAndDropItem.icon.activeSelf == true)                    // If icon inactive do not need to drop item into cell
            {
                if ((item != null) && (sourceCell != this))
                {
                    DropEventDescriptor desc = new DropEventDescriptor();
                    if (cellType == CellType.Swap)                                       // Check this cell's type
                    {
                        UpdateMyItem();
                        if (sourceCell.cellType == CellType.Swap)
                        {
                            desc.item = item;
                            desc.sourceCell = sourceCell;
                            desc.destinationCell = this;
                            SendRequest(desc);                      // Send drop request
                            StartCoroutine(NotifyOnDragEnd(desc));  // Send notification after drop will be finished
                            if (desc.permission == true)            // If drop permitted by application
                            {
                                if (myDadItem != null)            // If destination cell has item
                                {
                                    // Fill event descriptor
                                    DropEventDescriptor descAutoswap = new DropEventDescriptor
                                    {
                                        item = myDadItem,
                                        sourceCell = this,
                                        destinationCell = sourceCell
                                    };
                                    SendRequest(descAutoswap);                      // Send drop request
                                    StartCoroutine(NotifyOnDragEnd(descAutoswap));  // Send notification after drop will be finished
                                    if (descAutoswap.permission == true)            // If drop permitted by application
                                    {
                                        SwapItems(sourceCell, this);                // Swap items between cells
                                    }
                                    else
                                    {
                                        PlaceItem(item);            // Delete old item and place dropped item into this cell
                                    }
                                }
                                else
                                {
                                    PlaceItem(item);                // Place dropped item into this empty cell
                                }
                            }
                        }
                        else
                        {
                            desc.item = item;
                            desc.sourceCell = sourceCell;
                            desc.destinationCell = this;
                            SendRequest(desc);                      // Send drop request
                            StartCoroutine(NotifyOnDragEnd(desc));  // Send notification after drop will be finished
                            if (desc.permission == true)            // If drop permitted by application
                            {
                                PlaceItem(item);                    // Place dropped item into this cell
                            }
                        }
                    }
                }
            }
            if (item != null)
            {
                if (item.GetComponentInParent<DragAndDropCell>() == null)   // If item have no cell after drop
                {
                    Destroy(item.gameObject);                               // Destroy it
                }
            }
            UpdateMyItem();
            sourceCell.UpdateMyItem();
        }
    }

    /// <summary>
    /// Put item into this cell.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PlaceItem(DragAndDropItem item)
    {
        if (item != null)
        {
            DestroyItem();                                              // Remove current item from this cell
            myDadItem = null;
            DragAndDropCell cell = item.GetComponentInParent<DragAndDropCell>();
            if (cell != null)
            {
                if (cell.unlimitedSource == true)
                {
                    string itemName = item.name;
                    item = Instantiate(item);                               // Clone item from source cell
                    item.name = itemName;
                }
            }
            item.transform.SetParent(transform, false);
            item.transform.localPosition = Vector3.zero;
            item.MakeRaycast(true);
            myDadItem = item;
        }
    }

    /// <summary>
    /// Destroy item in this cell
    /// </summary>
    private void DestroyItem()
    {
        UpdateMyItem();
        if (myDadItem != null)
        {
            Destroy(myDadItem.gameObject);
        }
        myDadItem = null;
    }

    /// <summary>
    /// Send drag and drop request to application
    /// </summary>
    /// <param name="desc"> drag and drop event descriptor </param>
    /// <returns> result from desc.permission </returns>
    private bool SendRequest(DropEventDescriptor desc)
    {
        bool result = false;
        if (desc != null)
        {
            desc.triggerType = TriggerType.DropRequest;
            desc.permission = true;
            result = desc.permission;
        }
        return result;
    }

    /// <summary>
    /// Wait for event end and send notification to application
    /// </summary>
    /// <param name="desc"> drag and drop event descriptor </param>
    /// <returns></returns>
    private IEnumerator NotifyOnDragEnd(DropEventDescriptor desc)
    {
        // Wait end of drag operation
        while (DragAndDropItem.draggedItem != null)
        {
            yield return new WaitForEndOfFrame();
        }
        desc.triggerType = TriggerType.DropEventEnd;
    }

    /// <summary>
    /// Updates my item
    /// </summary>
    public void UpdateMyItem()
    {
        myDadItem = GetComponentInChildren<DragAndDropItem>();
    }

    /// <summary>
    /// Get item from this cell
    /// </summary>
    /// <returns> Item </returns>
    public DragAndDropItem GetItem()
    {
        return myDadItem;
    }

    /// <summary>
    /// Manualy add item into this cell
    /// </summary>
    /// <param name="newItem"> New item </param>
    public void AddItem(DragAndDropItem newItem)
    {
        if (newItem != null)
        {
            PlaceItem(newItem);
        }
    }

    /// <summary>
    /// Manualy delete item from this cell
    /// </summary>
    public void RemoveItem()
    {
        DestroyItem();
    }

    /// <summary>
    /// Swap items between two cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    public void SwapItems(DragAndDropCell firstCell, DragAndDropCell secondCell)
    {
        if ((firstCell != null) && (secondCell != null))
        {
            DragAndDropItem firstItem = firstCell.GetItem();                // Get item from first cell
            DragAndDropItem secondItem = secondCell.GetItem();              // Get item from second cell
                                                                            // Swap items
            if (firstItem != null)
            {
                firstItem.transform.SetParent(secondCell.transform, false);
                firstItem.transform.localPosition = Vector3.zero;
                firstItem.MakeRaycast(true);
            }
            if (secondItem != null)
            {
                secondItem.transform.SetParent(firstCell.transform, false);
                secondItem.transform.localPosition = Vector3.zero;
                secondItem.MakeRaycast(true);
            }
            // Update states
            firstCell.UpdateMyItem();
            secondCell.UpdateMyItem();
        }
    }


}

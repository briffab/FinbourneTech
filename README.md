# FinbourneTech
Please find in the repository two projects

  Cachelibrary          This is the cache compenent that contains the cache class and functions to add, get and remove items from the cache
  
  finbournecache       This is a c# solution that includes the cachelibrary project (you may need to reassign the dependency) and shows the cache having items added, accessed and removed
  

I have use the concurrent dictionary functionality in C# as this provides an already thread safe collection without any significant performance overhead and is considered by Microsoft as best practise for thread safe collections.

I have kept a log of cache item access events with the use of a linked list where the most recently accessed item is put to the end of the list sp that if the cache limit is hit, the item at the front of the list and therefore the item that is the oldest accessed is removed. The removed item is passed back to the item add function in order to serve as notification that an item has been removed.

In the example, the code is creating a cache with a limit of 1000 items. 1000 items are added, the type of which could be mixed but I have just used a string for the testing. The code then makes 10000 random accesses of the cache and then adds some more items. You can remove the comments slashes if you want to see the output to the console. Some items are then randomly removed and fianlly the cache is cleared.

If you have any troubke or questions please let me know.

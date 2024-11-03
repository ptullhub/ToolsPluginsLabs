How does A* pathfinding calculate and prioritize paths?
A* pathfinding calculates the cost for each node it traverses and creates a path based on the cheapest estimated cost

What challenges arise when dynamically updating obstacles in real-time?
The main challenge is regenerating a new path when map conditions change, such as a door closing or a wall being created. It cost performance to recalculate the next best
route and with a large pool of enemies in a scene it can get extremely taxing on performance if your world obstacles can change dynamically.

How could you adapt this code for larger grids or open-world settings?
I would probably make it use Greedy Best First instead of A*, it is less expensive to do so.

What would your approach be if you were to add weighted cells (e.g., "difficult terrain" areas)?
I would change the algorithm to account for these weighted cells in which they are avoided at all cost when pathfinding but if all avaliable nodes have to lead to one then
it will choose the path that has the least amount of weighted cells traversed.
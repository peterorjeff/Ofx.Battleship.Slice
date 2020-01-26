# Application Layer

This layer contains all application logic.

## Ship Collision Detection Logic

When adding a ship, collision detection is required before placing a new ship.
The linq expression used to determine this has a couple of steps that warrent explanation with examples.

```
var collisions = _context.Ships
    .Include(ship => ship.ShipParts)
    .Where(ship => ship.Board == board)
    .SelectMany(ship => ship.ShipParts, (ship, parts) => new
    {
        parts.X,
        parts.Y
    })
    .Where(part => xList.Contains(part.X) && yList.Contains(part.Y))
    .AsNoTracking();
```

The `xList` and `yList` collections are generated based on `Orientation` and `Length` from the ship request by the following logic:

```
var xList = new List<int>();
var yList = new List<int>();
switch(request.Orientation)
{
    case ShipOrientation.Horizontal:
        for (int i = 0; i < request.Length; i++) xList.Add(request.BowX + i);
        yList.Add(request.BowY);
        break;
    case ShipOrientation.Vertical:
        for (int i = 0; i < request.Length; i++) yList.Add(request.BowY + i);
        xList.Add(request.BowX);
        break;
}
```

Example request:

```
AttackX = 1
AttackY = 1
Length = 3
Orientation = Vertical
```

Will result in the following list values:

```
xlist = 1
ylist = 1,2,3
```

With the following existing ships:

```
1,1
2,1
3,1
```

The collisions where cluase `.Where(part => xList.Contains(part.X) && yList.Contains(part.Y))` results in the following evaluations:

```
where xlist contains 1 && ylist contains 1 = true
where xlist contains 2 && ylist contains 1 = false
where xlist contains 3 && ylist contains 1 = false
```

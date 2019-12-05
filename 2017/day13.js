/* jshint esversion:6 */
/* jshint -W087 */
debugger;
var input = `0: 3
1: 2
2: 6
4: 4
6: 4
8: 10
10: 6
12: 8
14: 5
16: 6
18: 8
20: 8
22: 12
24: 6
26: 9
28: 8
30: 8
32: 10
34: 12
36: 12
38: 8
40: 12
42: 12
44: 14
46: 12
48: 12
50: 12
52: 12
54: 14
56: 14
58: 14
60: 12
62: 14
64: 14
66: 17
68: 14
72: 18
74: 14
76: 20
78: 14
82: 18
86: 14
90: 18
92: 14`;

var scanners = [];
input.split("\n").map(function(line){ 
    var items = line.split(": ").concat([0]);
    var depth = parseInt(items[0]);
    var range = parseInt(items[1]);
    scanners[depth] = { depth: depth, range: range, location: 0, direction: 1 };
});

//console.log(scanners);

var delay = 1000000;
while(delay < 4000000)
{
    // for(var scanner of scanners)
    // {
    //     if(scanner != null)
    //     {
    //         if(scanner.range==2)
    //         {
    //             scanner.location = delay % 2;
    //             scanner.direction = scanner.location == 0 ? 1 : -1;
    //         }
    //         else
    //         {
    //             var totalDist = (scanner.range-1)*2;
    //             var currentLoc =  delay % totalDist;
    //             var maxLoc = scanner.range - 1;
    //             scanner.direction = currentLoc > maxLoc ? -1 : 1;
    //             scanner.location = scanner.direction == 1 ? currentLoc : maxLoc - (currentLoc-maxLoc);
    //         }
    //     }
    // }

    var caught = [];
    //for(var depth = 0; depth < scanners.length + delay; depth++)
    //for(var depth = 0; depth < scanners.length; depth++)
    for(var depth = delay; depth < scanners.length + delay; depth++)
    {
        var currentDepth = depth - delay;

        for(var scanner of scanners)
        {
            if(scanner != null)
            {
                if(scanner.range==2)
                {
                    scanner.location = depth % 2;
                    scanner.direction = scanner.location == 0 ? 1 : -1;
                }
                else
                {
                    var totalDist = (scanner.range-1)*2;
                    var currentLoc =  depth % totalDist;
                    var maxLoc = scanner.range - 1;
                    scanner.direction = currentLoc > maxLoc ? -1 : 1;
                    scanner.location = scanner.direction == 1 ? currentLoc : maxLoc - (currentLoc-maxLoc);
                }
            }
        }
    

        //var currentDepth = depth;
        //console.log("before:");
        //printScanners(currentDepth);
        if(currentDepth >= 0)
        {
            var closestScanner = scanners[currentDepth];
            if(closestScanner != null && closestScanner.location == 0)
            {
                caught.push({ depth: depth, range: closestScanner.range });
                break;
            }
        }
        
        // for(var scanner of scanners)
        // {
        //     if(scanner != null)
        //     {
        //         scanner.location += scanner.direction;
        //         if(scanner.location == scanner.range-1)
        //         {
        //             scanner.direction = -1;
        //         }
        //         else if(scanner.location == 0)
        //         {
        //             scanner.direction = 1;
        //         }
        //     }
        // }
        // console.log("after:");
        // printScanners(currentDepth);
        // console.log("----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
    }
    if(caught.length == 0)
    {
         break;
    }
    // console.log(caught);
    // var sum = 0;
    // for(var item of caught)
    // {
    //     sum += item.depth * item.range;
    // }
    // console.log(sum);

    delay++;
}

//console.log(3184);
//console.log(3878062)
console.log(delay);


function printScanners(depth)
{
    var line = "";
    for(var i = 0; i < scanners.length; i++)
    {
        line += " " + (i < 10 ? " " + i : i) + " ";
    }

    console.log(line);
    var maxRange = scanners.reduce((previous, current) => Math.max(previous, current.range), 0);
    for(var j = 0; j < maxRange; j++)
    {
        line = "";
        for(var s = 0; s < scanners.length; s++)
        {
            var scanner = scanners[s];
            if(scanner != null && scanner.range > j)
            {
                line += "[" + (scanner.location == j ? "S" : " ") + (depth == scanner.depth && j == 0 ? "X" : " " ) + "]";
            }
            else
            {
                line += "  " + (depth == s && j == 0 ? "X" : " " ) + " ";
            }
        }
        console.log(line);
    }

}

//const input = document.body.textContent.trim();
// const guards = input.split('\n').map(s => s.match(/\d+/g).map(Number));
// const caughtByGuard = delay => ([d, r]) => (delay + d) % (2 * (r - 1)) === 0;
// const severity = delay => guards.filter(caughtByGuard(delay))
//     .reduce((n, [d, r]) => n + d * r, 0);

// let delay = -1;
// while (guards.some(caughtByGuard(++delay)));
// console.log([severity(0), delay]);
/* jshint esversion:6 */
/* jshint -W087 */
debugger;

var input= `.########.....#...##.####
....#..#.#.##.###..#.##..
##.#.#..#.###.####.##.#..
####...#...####...#.##.##
..#...###.#####.....##.##
..#.##.######.#...###...#
.#....###..##....##...##.
##.##..####.#.######...##
#...#..##.....#..#...#..#
........#.##..###.#.....#
#.#..######.#.###..#...#.
.#.##.##..##.####.....##.
.....##..#....#####.#.#..
...#.#.#..####.#..###..#.
##.#..##..##....#####.#..
.#.#..##...#.#####....##.
.####.#.###.####...#####.
...#...######..#.##...#.#
#..######...#.####.#..#.#
...##..##.#.##.#.#.#....#
###..###.#..#.....#.##.##
..#....##...#..#..##..#..
.#.###.##.....#.###.#.###
####.##...#.#....#..##...
#.....#.#..#.##.#..###..#`;

// input = `..#
// #..
// ...`;

var map = input.split("\n").map(o => o.split(""));

var x = Math.floor(map[0].length/2);
var y = Math.floor(map.length/2);
var direction = 0;
var directions = [[-1,0], [0,1], [1,0], [0,-1]];
//var bursts = 10000;
var bursts = 10000000;
var infected = 0;
for(let b = 0; b < bursts; b++)
{
    if(x < 0)
    {
        x++;
        for(let y = 0; y < map.length; y++)
        {
            map[y].unshift(".");
        }
    }
    if(x > map[0].length-1)
    {
        for(let y = 0; y < map.length; y++)
        {
            map[y].push(".");
        }
    }
    if(y < 0)
    {
        y++;
        map.unshift([]);
        for(let x = 0; x < map[1].length; x++)
        {
            map[y].push(".");
        }
    }
    if(y > map.length-1)
    {
        map.push([]);
        for(let x = 0; x < map[0].length; x++)
        {
            map[y].push(".");
        }
    }
    var node = map[y][x];
    // if(node == "#")
    // {
    //     direction = mod(direction+1, 4);
    //     map[y][x] = ".";
    // }
    // else
    // {
    //     direction = mod(direction-1, 4);
    //     map[y][x] = "#";
    //     infected++;
    // }

    if(node == ".")
    {
        direction = mod(direction-1, 4);
        map[y][x] = "W";
    }
    else if(node == "W")
    {
        map[y][x] = "#";
        infected++;
    }
    else if(node == "#")
    {
        direction = mod(direction+1, 4);
        map[y][x] = "F";
    }
    else if(node == "F")
    {
        direction = mod(direction+2, 4);
        map[y][x] = ".";
    }


    x += directions[direction][1];
    y += directions[direction][0];

    //x = mod(x, map[0].length);
    //y = mod(y, map.length);
    //print();
}

console.log(infected);

function mod(n, m) {
    return ((n % m) + m) % m;
}

function print()
{
    console.log("--------------------------");
    for(let y = 0; y < map.length; y++)
    {
        var line ="";
        for(let x = 0; x < map.length; x++)
        {
            line += map[y][x];
        }
        console.log(line);
    }
}
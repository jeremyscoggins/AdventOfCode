/* jshint esversion:6 */
/* jshint -W087 */
debugger;

var input = `../.. => .../.#./.#.
#./.. => .../#../#..
##/.. => #.#/.#./.#.
.#/#. => ##./##./...
##/#. => .##/###/#..
##/## => .##/#../##.
.../.../... => .#.#/###./##.#/###.
#../.../... => #.#./..#./..../#.#.
.#./.../... => #.##/..#./.#.#/####
##./.../... => ###./.#../####/##..
#.#/.../... => ...#/####/#.##/...#
###/.../... => .#../..#./#..#/..#.
.#./#../... => ###./.##./#.../..#.
##./#../... => #.#./...#/..../.###
..#/#../... => ..../..../##../#..#
#.#/#../... => ..#./#..#/.#../..##
.##/#../... => ##../.#.#/.##./...#
###/#../... => ..../#.../#..#/#..#
.../.#./... => ##.#/#.#./#.../#..#
#../.#./... => ..#./#.#./.##./....
.#./.#./... => ..##/#.../..../###.
##./.#./... => .#../...#/.##./.#.#
#.#/.#./... => ...#/#..#/.#../.###
###/.#./... => ###./.###/##.#/#.##
.#./##./... => ##.#/##../..##/..##
##./##./... => #.##/.###/.##./###.
..#/##./... => ##.#/.##./..##/####
#.#/##./... => ####/####/#.##/.#..
.##/##./... => ####/.#../####/#..#
###/##./... => #.#./..#./###./..#.
.../#.#/... => #.../..../.#../#.##
#../#.#/... => ..#./###./####/..#.
.#./#.#/... => #.##/.#../##.#/#.#.
##./#.#/... => ###./.###/###./##..
#.#/#.#/... => ...#/.##./.#.#/#.##
###/#.#/... => ####/#.../###./###.
.../###/... => ..##/#.##/.#../.#..
#../###/... => ..../.###/.#.#/...#
.#./###/... => #.##/.#.#/.#.#/.##.
##./###/... => #..#/.#.#/#.##/#.#.
#.#/###/... => #.../##../#.##/##.#
###/###/... => .#../.#../.###/..#.
..#/.../#.. => ...#/.##./.##./####
#.#/.../#.. => ##.#/##../#.#./.#..
.##/.../#.. => #..#/.##./####/.#..
###/.../#.. => ..../..../..##/..##
.##/#../#.. => ..##/.##./#..#/###.
###/#../#.. => ##.#/#..#/#.../#..#
..#/.#./#.. => #..#/##.#/.##./#..#
#.#/.#./#.. => .#../####/..##/#.##
.##/.#./#.. => ###./#..#/.##./###.
###/.#./#.. => ####/###./##../..##
.##/##./#.. => #.../####/...#/####
###/##./#.. => .#../#.##/.##./####
#../..#/#.. => .#../####/#.../....
.#./..#/#.. => .#.#/...#/.###/.#.#
##./..#/#.. => ..##/#..#/#..#/....
#.#/..#/#.. => .###/.#.#/.##./#.#.
.##/..#/#.. => ...#/#.##/#.../..##
###/..#/#.. => #.##/#.##/...#/#.##
#../#.#/#.. => #..#/..##/.#../.###
.#./#.#/#.. => #.##/..../.##./.#..
##./#.#/#.. => #.#./..#./.#.#/.#..
..#/#.#/#.. => ...#/#..#/###./##..
#.#/#.#/#.. => ##.#/##.#/.#.#/.#..
.##/#.#/#.. => #..#/#..#/##../.#..
###/#.#/#.. => #.##/..##/##.#/....
#../.##/#.. => ##.#/.##./...#/.#.#
.#./.##/#.. => .##./.###/###./.#.#
##./.##/#.. => #.#./#.##/..##/.#..
#.#/.##/#.. => ..#./.##./..##/.#..
.##/.##/#.. => ##../..##/#..#/#...
###/.##/#.. => ###./#..#/##.#/..#.
#../###/#.. => .###/#.../####/#.#.
.#./###/#.. => #.#./.###/#..#/....
##./###/#.. => ..#./.#.#/#.../#...
..#/###/#.. => ...#/..#./##../#..#
#.#/###/#.. => .#.#/###./.#../##..
.##/###/#.. => .#../###./..#./##..
###/###/#.. => .#../..##/#.../#...
.#./#.#/.#. => ##.#/..../##../.#..
##./#.#/.#. => #.../#.##/.###/#.##
#.#/#.#/.#. => ...#/..##/##.#/#.##
###/#.#/.#. => ...#/.#.#/###./#..#
.#./###/.#. => ...#/...#/##../#.##
##./###/.#. => ###./###./.#.#/..##
#.#/###/.#. => ..../#..#/..##/#..#
###/###/.#. => .#.#/.#.#/##../.###
#.#/..#/##. => .##./..#./##../....
###/..#/##. => ####/...#/.#.#/#...
.##/#.#/##. => ..#./...#/###./.#..
###/#.#/##. => ..../.#../.#../#.#.
#.#/.##/##. => .##./..../#.../.#.#
###/.##/##. => ..../#..#/...#/#...
.##/###/##. => #.##/##.#/#.../..#.
###/###/##. => .#../.###/###./##.#
#.#/.../#.# => .#.#/..../#..#/.#..
###/.../#.# => ##../#.##/##.#/..#.
###/#../#.# => .#.#/..../.#.#/.###
#.#/.#./#.# => ...#/..../##.#/#...
###/.#./#.# => ####/.###/#.#./#.##
###/##./#.# => #..#/.###/...#/###.
#.#/#.#/#.# => #.##/...#/.###/.##.
###/#.#/#.# => #.../.#.#/.#.#/.###
#.#/###/#.# => ##.#/##../###./#...
###/###/#.# => .##./.###/.#../..##
###/#.#/### => #.##/###./#..#/#..#
###/###/### => #.../..../#..#/#...`;

var inputArray = input.split("\n");

var rules = {};
for(let i = 0; i < inputArray.length; i++)
{
    var line = inputArray[i];
    var parts = line.split(" => ");
    rules[parts[0]] = parts[1].split("/").map(o => o.split(""));
}

console.log(rules);


var initial = [[".", "#", "."],
               [".", ".", "#"],
               ["#", "#", "#"]];

// console.log("----------0------------");
// print(initial);
// console.log(hash(initial));
// console.log("----------1------------");
// initial = transpose(initial);
// print(initial);
// console.log("----------2------------");
// initial = flip(initial);
// print(initial);
// console.log("----------3------------");
// initial = transpose(initial);
// print(initial);
// console.log("----------4------------");
// initial = flip(initial);
// print(initial);
// console.log("----------5------------");
// initial = transpose(initial);
// print(initial);
// console.log("----------6------------");
// initial = flip(initial);
// print(initial);
// console.log("----------7------------");
// initial = transpose(initial);
// print(initial);
// console.log("-----------------------");

var iterations = 18;

for(let i = 0; i < iterations; i++)
{
    print(initial);
    var test = [[initial]];
    var didSplit = false;
    // if(initial.length > 4 && initial.length % 4 == 0)
    // {
    //     test = split(initial, 4);
    //     var found = enumerateAndFind(test);
    //     initial = join(test);
    //     if(found)
    //     {
    //         continue;
    //     }
    // }
    if(initial.length > 2 && initial.length % 2 == 0)
    {
        test = split(initial, 2);
        var found = enumerateAndFind(test);
        initial = join(test);
        if(found)
        {
            continue;
        }
    }
    if(initial.length > 3 && initial.length % 3 == 0)
    {
        test = split(initial, 3);
        var found = enumerateAndFind(test);
        initial = join(test);
        if(found)
        {
            continue;
        }
    }
    var found = enumerateAndFind(test);
    initial = join(test);
    // for(let y = 0; y < test.length; y++)
    // {
    //     for(let x = 0; x < test[y].length; x++)
    //     {
    //         var match = findMatch(test[x][y]);
    //         if(match == null)
    //         {
    //             console.log("oh no!");
    //         }
    //         else
    //         {
    //             test[x][y] = match;
    //         }
    //     }
    // }
    //print(initial);
}

print(initial);

var pixelCount = 0;
for(let y = 0; y < initial.length; y++)
{
    for(let x = 0; x < initial[y].length; x++)
    {
        if(initial[y][x] == "#")
        {
            pixelCount++;
        }
    }
}


console.log("done: " + pixelCount);

function enumerateAndFind(test)
{
    var foundMatch = false;
    for(let y = 0; y < test.length; y++)
    {
        for(let x = 0; x < test[y].length; x++)
        {
            var match = findMatch(test[x][y]);
            if(match != null)
            {
                test[x][y] = match;
                foundMatch = true;
            }
        }
    }
    return foundMatch;
}

function split(mat, size)
{
    var currentSize = mat.length;
    var innerSize = size;
    var outerSize = currentSize / size;
    var newMat = new Array(outerSize);
    for(let y = 0; y < outerSize; y++)
    {
        newMat[y] = new Array(outerSize);
        for(let x = 0; x < outerSize; x++)
        {
            newMat[y][x] = new Array(innerSize);
            for(let y2 = 0; y2 < innerSize; y2++)
            {
                newMat[y][x][y2] = new Array(innerSize);
                for(let x2 = 0; x2 < innerSize; x2++)
                {
                    newMat[y][x][y2][x2] = mat[(y * innerSize) + y2][(x * innerSize) + x2];
                }
            }
        }
    }
    return newMat;
}

function join(mat)
{
    if(mat.length == 1)
    {
        return mat[0][0];
    }
    var outerSize = mat.length;
    var innerSize = mat[0][0].length;
    var newSize = innerSize * outerSize;
    var newMat = new Array(newSize);
    for(let y = 0; y < newSize; y++)
    {
        newMat[y] = new Array(newSize);
        for(let x = 0; x < newSize; x++)
        {
            newMat[y][x] = mat[Math.floor(y/parseFloat(innerSize))][Math.floor(x/parseFloat(innerSize))][y % innerSize][x % innerSize];
        }
    }
    return newMat;
}

function findMatch(mat)
{
    var key = hash(mat);
    if(rules.hasOwnProperty(key))
    {
        //console.log("match 0: ", rules[key]);
        return rules[key];
    }
    mat = transpose(mat);
    key = hash(mat);
    if(rules.hasOwnProperty(key))
    {
        //console.log("match 1: ", rules[key]);    
        return rules[key];
    }
    mat = flip(mat);
    key = hash(mat);
    if(rules.hasOwnProperty(key))
    {
        //console.log("match 2: ", rules[key]);    
        return rules[key];
    }
    mat = transpose(mat);
    key = hash(mat);
    if(rules.hasOwnProperty(key))
    {
        //console.log("match 3: ", rules[key]);    
        return rules[key];
    }
    mat = flip(mat);
    key = hash(mat);
    if(rules.hasOwnProperty(key))
    {
        //console.log("match 4: ", rules[key]);    
        return rules[key];
    }
    mat = transpose(mat);
    key = hash(mat);
    if(rules.hasOwnProperty(key))
    {
        //console.log("match 5: ", rules[key]);    
        return rules[key];
    }
    mat = flip(mat);
    key = hash(mat);
    if(rules.hasOwnProperty(key))
    {
        //console.log("match 6: ", rules[key]);    
        return rules[key];
    }
    mat = transpose(mat);
    key = hash(mat);
    if(rules.hasOwnProperty(key))
    {
        //console.log("match 7: ", rules[key]);    
        return rules[key];
    }
    return null;
}

function flip(mat)
{
    var newMat = new Array(mat.length);
    for(let y = 0; y < mat.length; y++)
    {
        newMat[y] = new Array(mat[y].length);
        for(let x = 0; x < mat[y].length; x++)
        {
            var newX = (mat[y].length-1) - x;
            newMat[y][newX] = mat[y][x];
        }
    }
    return newMat;
}


function transpose(mat)
{
    var newMat = [];
    for(let y = 0; y < mat.length; y++)
    {
        newMat[y] = new Array(mat[y].length);
        for(let x = 0; x < mat[y].length; x++)
        {
            newMat[y][x] = mat[x][y];
        }
    }
    return newMat;
}

function print(mat)
{
    console.log("------------------------------------------");
    for(let y = 0; y < mat.length; y++)
    {
        var line = "";
        for(let x = 0; x < mat[y].length; x++)
        {
            line += mat[y][x];
        }
        console.log(line);
    }
}

function hash(mat)
{
    return mat.map(o => o.join("")).join("/");
}
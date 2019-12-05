/* jshint esversion:6 */
/* jshint -W087 */
debugger;
var input = "jxqlasbh";

var grid = [];
var count = 0;
for(var y = 0; y < 128; y++)
{
    var hashInput = input + "-" + y;
    var hash = calculateHash(hashInput);
    grid[y] = [];
    for(var x = 0; x < 128; x+=4)
    {
        var byte = parseInt(hash.charAt(x/4), 16);
        var bits = ("000" + byte.toString(2)).substr(-4);
        for(var i = 0; i < 4; i++)
        {
            grid[y][x+i] = parseInt(bits[i]);
         
            if(grid[y][x+i] == 1)
            {
                count++;
            }
        }
    }
}
console.log(count);

var seen = {};
var regions = [];
var region = [];

//while(Object.keys(seen).length < (128 * 128))
do
{
    var regionNumber = regions.length + 1;
    region = [];
    var found = false;
    do
    {
        found = false;
        for(var y = 0; y < 128; y++)
        {
            for(var x = 0; x < 128; x++)
            {
                var key = "["+y+"]["+x+"]";
                if(!seen.hasOwnProperty(key))
                {
                    if(grid[y][x] == 0)
                    {
                        seen[key] = 0;
                    }
                    else
                    {
                        if(region.length == 0)
                        {
                            //first one
                            seen[key] = regionNumber;
                            region.push({x:x, y:y});
                            found = true;
                            console.log("added "+key+" to region: " + regionNumber);
                        }
                        else
                        {
                            //adjacent to anythingelse in this region
                            for(var item of region)
                            {
                                if(item.x != x || item.y != y)
                                {
                                    if (item.x == x && Math.abs(y - item.y) <= 1)
                                    {
                                        seen[key] = regionNumber;
                                        region.push({x:x, y:y});
                                        found = true;
                                        console.log("added "+key+" to region: " + regionNumber);
                                    }
                                    else if (item.y == y && Math.abs(x - item.x) <= 1)
                                    {
                                        seen[key] = regionNumber;
                                        region.push({x:x, y:y});
                                        found = true;
                                        console.log("added "+key+" to region: " + regionNumber);
                                    }
                                }
                                if(found)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if(found)
                    {
                        break;
                    }
                }
                if(found)
                {
                    break;
                }
            }
        }
    } while (found);
    if(region.length > 0)
    {
        regions.push(region);
        console.log("added region: " + regionNumber);
    }
} while(region.length);

console.log(regions);


function calculateHash(inputString)
{

    var input = [];
    
    //part 2
    for(var i = 0; i < inputString.length; i++)
    {
        input.push(inputString.charCodeAt(i));
    }
    input = input.concat([17, 31, 73, 47, 23]);
    
    
    var list = [];
    
    for(var j = 0; j < 256; j++)
    {
        list[j] = j;    
    }
    
    var position = 0;
    var skipSize = 0;
    
    //console.log(list);
    
    for(var r = 0; r < 64; r++)
    {
        for(var k = 0; k < input.length; k++)
        {
            var size = input[k];
            var start = position;
            var end = start + size;
            reverse(list, start, end);
            position += size + skipSize;
            skipSize++;
        }
    }
    
    //console.log(list);
    //console.log("result : " + (list[0] * list[1]));
    
    var blocks = [];
    var hexString = "";
    
    for(var l = 0; l < list.length; l+=16)
    {
        var block = list[l] ^ list[l+1] ^ list[l+2] ^ list[l+3] ^ list[l+4] ^ list[l+5] ^ list[l+6] ^ list[l+7] ^ list[l+8] ^ list[l+9] ^ list[l+10] ^ list[l+11] ^ list[l+12] ^ list[l+13] ^ list[l+14] ^ list[i+15];
        blocks.push(block);
        hexString += ("0" + block.toString(16)).substr(-2);
    }
    //console.log(hexString);
    return hexString;
}

function reverse(list, start, end)
{
    var numbers = [];
    for(var i = start; i < end; i++)
    {
        numbers.push(list[i % list.length]);
    }

    for(var j = start; j < end; j++)
    {
        list[j % list.length] = numbers.pop();
    }
}

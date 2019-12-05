/* jshint esversion:6 */
/* jshint -W087 */
debugger;
var inputString = "106,16,254,226,55,2,1,166,177,247,93,0,255,228,60,36";
var input = [];

//part 2
for(var i = 0; i < inputString.length; i++)
{
    input.push(inputString.charCodeAt(i));
}
input = input.concat([17, 31, 73, 47, 23]);


var list = [];

for(var i = 0; i < 256; i++)
{
    list[i] = i;    
}

var position = 0;
var skipSize = 0;

console.log(list);

for(var r = 0; r < 64; r++)
{
    for(var i = 0; i < input.length; i++)
    {
        var size = input[i];
        var start = position;
        var end = start + size;
        reverse(start, end);
        position += size + skipSize;
        skipSize++;
    }
}

console.log(list);
console.log("result : " + (list[0] * list[1]));

var blocks = [];
var hexString = "";

for(var i = 0; i < list.length; i+=16)
{
    var block = list[i] ^ list[i+1] ^ list[i+2] ^ list[i+3] ^ list[i+4] ^ list[i+5] ^ list[i+6] ^ list[i+7] ^ list[i+8] ^ list[i+9] ^ list[i+10] ^ list[i+11] ^ list[i+12] ^ list[i+13] ^ list[i+14] ^ list[i+15];
    blocks.push(block);
    hexString += ("0" + block.toString(16)).substr(-2);
}
console.log(hexString);

function reverse(start, end)
{
    var numbers = [];
    for(var i = start; i < end; i++)
    {
        numbers.push(list[i % list.length]);
    }

    for(var i = start; i < end; i++)
    {
        list[i % list.length] = numbers.pop();
    }
}

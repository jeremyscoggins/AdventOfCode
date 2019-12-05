/* jshint esversion:6 */
/* jshint -W087 */
debugger;

// 65  64  63  62  61  60  59  58  57
// 66  37  36  35  34  33  32  31  56
// 67  38  17  16  15  14  13  30  55
// 68  39  18   5   4   3  12  29  54
// 69  40  19   6   1   2  11  28  53
// 70  41  20   7   8   9  10  27  52
// 71  42  21  22  23  24  25  26  51
// 72  43  44  45  46  47  48  49  50
// 73  74  75  76  77  78  79  80  81 
 
//ring 1 corners: 1,1  -1,1  -1,-1  1, -1
//ring 2 corners: 2,2  -2,2  -2,-2  2, -2

 
 
 
//var c = document.getElementById("canvas");
//var ctx = c.getContext("2d");
var target = 277678;
var current = 2;
var local = 1;
var side = 3;
var ring = 1;
var x = 1;
var y = 1;
var dir = 0; //0 = up, 1 = right, 2 = down, 3 = left
var grid = [[1,0,0]];
//var work = function () {

  while(current <= target)
  {
    //move
    var turn = false;
    switch(dir)
    {
      case 0:
        y--;
        turn = Math.abs(y) >= ring;
        break;
      case 1:
        x++;
        turn = Math.abs(x) >= ring;
        break;
      case 2:
        y++;
        turn = Math.abs(y) >= ring;
        break;
      case 3:
        x--;
        turn = Math.abs(x) >= ring;
        break;
    }
    //ctx.fillRect(x * 10 + 500, y * 10 + 500, 8, 8);
    //console.log("current: " + current + " ring: " + ring + " dir: " + dir + " x: " + x + " y: " + y);
    var ringSize = (side - 1) * 4;
    if(local == ringSize - 1)
    {
      //console.log("new ring");
      //new ring
      ring++;
      side+=2;
      local = 0;
    }
    if(turn)
    {
      //console.log("turn");
      //corner
      dir--;
      if(dir < 0)
      {
        dir = 3;
      }
    }
  
    local++;
    current++;
    //setTimeout(work, 10);
    var current2 = 0;
    for(var item in grid)
    {
      if(Math.abs(x - grid[item][1]) <= 1 && Math.abs(y - grid[item][2]) <= 1)
      {
        current2 += grid[item][0];
      }
    }
    grid.push([current2, x, y]);
    if(current2 > target)
    {
      break;
    }
  }
//};
//work();

console.log("current: " + current + " ring: " + ring + " dir: " + dir + " x: " + x + " y: " + y + " dist: " + (x+y));
    
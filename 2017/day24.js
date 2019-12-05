/* jshint esversion:6 */
/* jshint -W087 */
debugger;

var input = `32/31
2/2
0/43
45/15
33/24
20/20
14/42
2/35
50/27
2/17
5/45
3/14
26/1
33/38
29/6
50/32
9/48
36/34
33/50
37/35
12/12
26/13
19/4
5/5
14/46
17/29
45/43
5/0
18/18
41/22
50/3
4/4
17/1
40/7
19/0
33/7
22/48
9/14
50/43
26/29
19/33
46/31
3/16
29/46
16/0
34/17
31/7
5/27
7/4
49/49
14/21
50/9
14/44
29/29
13/38
31/11`;

var connections = input.split("\n").map((o,n) => { return { index: n, a: parseInt(o.split("/")[0]), b: parseInt(o.split("/")[1]) }; });
connections.sort((a, b) => a.a - b.a || a.b - b.b);
console.log(connections);

var bridges = [];
var max = 0;
var starts = connections.filter(o => o.a == 0 || o.b == 0);
for(let start of starts)
{
    walk([start], start.a != 0 ? start.a : start.b, start.a + start.b);
}

console.log(max);

bridges.sort((a, b) => a.length - b.length || a.strength - b.strength);

console.log(bridges[bridges.length-1]);

function walk(current, output, sum)
{
    var walking = false;
    for(let connection of connections)
    {
        if(connection.a == output || connection.b == output)
        {
            if(current.findIndex(o => o.index == connection.index) == -1)
            {
                var newOutput = connection.a != output ? connection.a : connection.b;
                if(newOutput != 0)
                {
                    var next = current.concat([connection]);
                    walk(next, newOutput, sum+(connection.a + connection.b));
                    walking = true;
                }
            }
        }
    }
    if(!walking)
    {
        //console.log(sum);
        max = Math.max(max, sum);
        bridges.push({ strength: sum, length: current.length });
    }
}
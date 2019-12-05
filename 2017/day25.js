/* jshint esversion:6 */
/* jshint -W087 */
debugger;

/*

Begin in state A.
Perform a diagnostic checksum after 12134527 steps.

In state A:
If the current value is 0:
- Write the value 1.
- Move one slot to the right.
- Continue with state B.
If the current value is 1:
- Write the value 0.
- Move one slot to the left.
- Continue with state C.

In state B:
If the current value is 0:
- Write the value 1.
- Move one slot to the left.
- Continue with state A.
If the current value is 1:
- Write the value 1.
- Move one slot to the right.
- Continue with state C.

In state C:
If the current value is 0:
- Write the value 1.
- Move one slot to the right.
- Continue with state A.
If the current value is 1:
- Write the value 0.
- Move one slot to the left.
- Continue with state D.

In state D:
If the current value is 0:
- Write the value 1.
- Move one slot to the left.
- Continue with state E.
If the current value is 1:
- Write the value 1.
- Move one slot to the left.
- Continue with state C.

In state E:
If the current value is 0:
- Write the value 1.
- Move one slot to the right.
- Continue with state F.
If the current value is 1:
- Write the value 1.
- Move one slot to the right.
- Continue with state A.

In state F:
If the current value is 0:
- Write the value 1.
- Move one slot to the right.
- Continue with state A.
If the current value is 1:
- Write the value 1.
- Move one slot to the right.
- Continue with state E.

*/

const right = 1;
const left = -1;
const steps = 12134527;
var rules = {
    "A" : {
        0 : {
            newVal : 1,
            move : right,
            next: "B"
        },
        1 : {
            newVal : 0,
            move : left,
            next: "C"
        }
    },
    "B" : {
        0 : {
            newVal : 1,
            move : left,
            next: "A"
        },
        1 : {
            newVal : 1,
            move : right,
            next: "C"
        }
    },
    "C" : {
        0 : {
            newVal : 1,
            move : right,
            next: "A"
        },
        1 : {
            newVal : 0,
            move : left,
            next: "D"
        }
    },
    "D" : {
        0 : {
            newVal : 1,
            move : left,
            next: "E"
        },
        1 : {
            newVal : 1,
            move : left,
            next: "C"
        }
    },
    "E" : {
        0 : {
            newVal : 1,
            move : right,
            next: "F"
        },
        1 : {
            newVal : 1,
            move : right,
            next: "A"
        }
    },
    "F" : {
        0 : {
            newVal : 1,
            move : right,
            next: "A"
        },
        1 : {
            newVal : 1,
            move : right,
            next: "E"
        }
    }
};

var state = "A";
var tape = [0];
var current = 0;


for(let step = 0; step < steps; step++)
{
    if(current < 0)
    {
        tape.unshift(0);
        current = 0;
    }
    else if(current > tape.length-1)
    {
        tape.push(0);
    }
    let value = tape[current];
    let rule = rules[state];
    let valRule = rule[value];
    tape[current] = valRule.newVal;
    current += valRule.move;
    state = valRule.next;
}

let checksum = tape.reduce((prev, current) => prev+current, 0);

console.log(checksum);
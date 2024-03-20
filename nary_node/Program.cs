using nary_node;

var root = new NaryNode<string>("Root");
var a = new NaryNode<string>("A");
var b = new NaryNode<string>("B");
var c = new NaryNode<string>("C");
var d = new NaryNode<string>("D");
var e = new NaryNode<string>("E");
var f = new NaryNode<string>("F");
var g = new NaryNode<string>("G");
var h = new NaryNode<string>("H");
var i = new NaryNode<string>("I");

root.AddChild(a);
root.AddChild(b);
root.AddChild(c);

a.AddChild(d);
a.AddChild(e);
d.AddChild(g);
c.AddChild(f);
f.AddChild(h);
f.AddChild(i);

Console.WriteLine(root);
Console.WriteLine(a);
Console.WriteLine(b);
Console.WriteLine(c);
Console.WriteLine(d);
Console.WriteLine(e);
Console.WriteLine(f);
Console.WriteLine(g);
Console.WriteLine(h);
Console.WriteLine(i);

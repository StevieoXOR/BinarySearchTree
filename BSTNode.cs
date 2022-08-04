//IN C#:  Objects are ALWAYS passed as pointers/references, never copies
//IN C#:  Avoid using unsafe{} unless ABSOLUTELY NECESSARY (95% of cases)
//Printing an object prints the name of the TYPE of object (i.e. prints the name of the class)
//E.g. Console.WriteLine("{0},{1},  {2},{3}\n",  Datum,Name, LeftNode,RightNode);   LeftNode and RightNode (if not null) print out as "Node"
//public DataTypeLikeInt? VariableOrObjectName = Datum;
//  The "?" indicates a nullable field, which lets the constructor exit with null fields (i.e. variables or objects set to null)
public class Node
{
	public int Datum = 0;
	public String Name = "Default";
	public Node? LeftChildNode  = null;	//Used for fast searching in a binary search tree
	public Node? RightChildNode = null;	//Used for fast searching in a binary search tree
	public bool IsLeafNode = true;		//Used only for the binary search tree, not used for Connections[]
	//public Node[] Connections = new Node[5/*ArraySize_ie_ArrayLength*/];
	public Node?[] Connections = {null,null,null,null,null};

	public Node(){}								//Default Constructor
	public Node(int d, String n)				//Nondefault Constructor
	{
		/*this.*/Datum = d;
		Name = n;
		//Already null by default when I instantiated the variable, hence why  LeftChildNode = null;  isn't included in the constructors
		//LeftChildNode  = null;
	}
	public Node(int d, String n, Node[] c)				//Nondefault Constructor
	{
		/*this.*/Datum = d;
		Name = n;
		Connections = c;
	}

	public Node(int datum, String name, Node leftNode, Node rightNode, Node[] connections)	//Nondefault Constructor
	{
		/*this.*/Datum = datum;
		Name = name;
		LeftChildNode  = leftNode;
		RightChildNode = rightNode;
		Connections = connections;
	}

	public String ConnectionsToString()
	{
		string s = "";
		int lenSub1 = GetAryLen(Connections)-1;
		for(int i=0; i<lenSub1; i++)
			{if(Connections[i]!=null){					s += (Connections[i].Datum + ", ");}}
		if(lenSub1>-1 && Connections[lenSub1]!=null){	s += Connections[lenSub1].Datum;}
		return (String)(s);
	}
	public int GetAryLen(Node[] conxns)
	{
		int maxLen_capacity = conxns.Length;
		int actualLen = 0;
		for(int i=0; i<maxLen_capacity; i++)
			{if(conxns[i]!=null){actualLen++;}}
		return actualLen;
	}
	public void Update_IsLeafNode()
	{
		String ln="", rn="";
		if(LeftChildNode !=null){	ln  = LeftChildNode.Name;}
		if(RightChildNode!=null){	rn  = RightChildNode.Name;}
		
		//If ALL Node paths have empty Strings as names, then currNode is a leaf Node
		//If even a single Node path doesn't have an empty String as a name, then currNode is NOT a leaf Node
		if(ln=="" && rn==""){	/*currNodeObject.*/IsLeafNode = true;}
		else{					/*currNodeObject.*/IsLeafNode = false;}
	}
	public void Print()
	{
		String ln="", rn="";
		if(LeftChildNode   !=null){	ln  = LeftChildNode.Name;}
		if(RightChildNode  !=null){	rn  = RightChildNode.Name;}
		
		Console.WriteLine("Datum,       Name,    LeftNode, RightNode, IsLeafNode,  Connections");
		Console.WriteLine("{0,5}, {1,10},  {2,10}, {3,10}, {4,10}, {5}\n",Datum,Name,ln,rn,IsLeafNode,ConnectionsToString());
	}
}	//END class Node{}
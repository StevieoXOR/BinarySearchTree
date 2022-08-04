/*
                 _______________________33_______________________
			    /                                                \
		 ______16______                                   ________92________
	    /              \                                 /                  \
14(<33 && <16)   16(<33 && >=16)                34(>=33 && <92)      93(>=33 && >=92)
*/
public class BSTree
{
	private bool debugIsEnabled = false;
	private Node Root = new Node(-1,"ROOT");



	public Node SearchFor(int numToFind)
	{
		Node currNode = Root;
		Node parentOfCurrNode = null;
		while(currNode!=null && currNode.Datum!=numToFind)
		{
			parentOfCurrNode = currNode;		//Updates parentNode BEFORE altering currNode
				  if(numToFind< currNode.Datum){	currNode = currNode.LeftChildNode;}
			else/*if(numToFind>=currNode.Datum)*/{	currNode = currNode.RightChildNode;}
		}
		//currNode is now null, unless the # was found(currNode==NodeThatHasTheWanted#)
		if(currNode==null)
		{
			if(parentOfCurrNode!=null)
			{
				if(debugIsEnabled){Console.WriteLine($"SearchFor({numToFind}):   Found Node == {parentOfCurrNode.Datum}");}
				return parentOfCurrNode;
			}
			else{return Root;}
		}
		else
		{
			if(debugIsEnabled){Console.WriteLine($"SearchFor({numToFind}):   Found Node == {currNode.Datum}");}
			return currNode;
		}
	}

	//Find the deepest depth Leaf Node that is closest to the numToFind
	public Node SearchFor_LeafNode(int numToFind)
	{
		Node currNode = Root;
		Node parentOfCurrNode = null;
		while(currNode!=null)
		{
			parentOfCurrNode = currNode;		//Updates parentNode BEFORE altering currNode
				  if(numToFind< currNode.Datum && currNode.RightChildNode==null){	currNode = currNode.LeftChildNode;}
			//currNode.RightChildNode==null  because duplicate #s go to the RightChildNode
			else/*if(numToFind>=currNode.Datum || currNode.RightChildNode!=null)*/{	currNode = currNode.RightChildNode;}
		}
		//currNode is now null
		if(parentOfCurrNode!=null)
		{
			if(debugIsEnabled){Console.WriteLine($"SearchFor_LeafNode({numToFind}):   Found Leaf Node == {parentOfCurrNode.Datum}");}
			return parentOfCurrNode;
		}
		else{return Root;}
	}


	/*			ADD NODE WITH DATUM==36
    1)          ____________________33____________________
			   /                                          \
		______16______                              ______92______[Node 92  ==  parentOfNodeToAdd = SearchFor(36);]
	   /              \                            /              \
    null               null                     null               null

    2)          ____________________33____________________
			   /                                          \
		______16______                              ______92______[Node 92  ==  parentOfNodeToAdd = SearchFor(36);]
	   /              \                            /              \
    null               null                  36[36<92]             null
	*/
	public void AddNode(Node NodeToAdd)
	{
		int numToAdd = NodeToAdd.Datum;

		/*			IF TRYING TO ADD A NODE THAT ALREADY EXISTS, THIS BLOCK OF CODE OVERWRITES THE EXISTING NODE,
					CAUSING AN INFINITE ADDITION TO THE TREE.
		//Prevent weird cyclical patterns (well that's ironic)
		NodeToAdd.LeftChildNode = null;
		NodeToAdd.RightChildNode = null;*/

		//To avoid infinite circular recursion, check that the address of the Node to add doesn't already exist in the tree
		if(SearchFor(numToAdd)==NodeToAdd)	//if(addressInTree==addressOfNodeToAdd)
		{
			Console.WriteLine("EXACT SAME NODE ALREADY EXISTS. COULD NOT ADD NODE {0} TO TREE",numToAdd);
			return;
		}

		Node parentOfNodeToAdd = SearchFor_LeafNode(numToAdd);	//Find the leaf Node where NodeToAdd should be added onto.
		int parentNum = parentOfNodeToAdd.Datum;
		if(numToAdd<parentNum && parentOfNodeToAdd.LeftChildNode==null)
		{
			if(debugIsEnabled){Console.WriteLine("Added Node {0} to tree on left side of leaf node {1}",numToAdd,parentNum);}
			parentOfNodeToAdd.LeftChildNode  = NodeToAdd;
		}
		else if(numToAdd>=parentNum && parentOfNodeToAdd.RightChildNode==null)
		{
			if(debugIsEnabled){Console.WriteLine("Added Node {0} to tree on right side of leaf node {1}",numToAdd,parentNum);}
			parentOfNodeToAdd.RightChildNode = NodeToAdd;
		}
		else{Console.WriteLine("UNABLE TO ADD NODE");}
	}

	public void Print()
	{
		Print(Root,0);
		Console.WriteLine("\n");	//2 newlines
	}
	public void Print(Node currNode, int depth)
	{
		if(currNode!=null)
		{
			int ld=-1, cd=currNode.Datum, rd=-1;
			if(currNode.LeftChildNode!=null){	ld = currNode.LeftChildNode.Datum;}
			if(currNode.RightChildNode!=null){	rd = currNode.RightChildNode.Datum;}
			String cnxuns = currNode.ConnectionsToString();

			if(debugIsEnabled){Console.WriteLine("Called PrintLeftNode");}
			Print(currNode.LeftChildNode, depth+1);											//Print LeftChild of currNode
			if(depth!=0){	Console.WriteLine("{0}{1}: L{2},R{3}, Cnxuns[{4}]", Indent(depth),cd,ld,rd,cnxuns);}	//Print currNode
			else{			Console.WriteLine("ROOT");}
			if(debugIsEnabled){Console.WriteLine("Called PrintRightNode");}
			Print(currNode.RightChildNode,depth+1);											//Print RightChild of currNode
		}
	}
	private String Indent(int depth)
	{
		string s = "";
		for(int i=0; i<depth; i++)
			{s += "  |";}
		return (String)(s);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum NodeType {Room, Corridor}

enum EdgeDirection {Top, Right, Bottom, Left};
enum NodeType {Room, Corridor};

//struct Edge
//{
//    public List<Vector2> vertex;

//    public Edge(Vector2 v1, Vector2 v2)
//    {
//        vertex = new List<Vector2> { v1, v2 };
//    }
//};

struct Edge
{
    public Vector2 v1;
    public Vector2 v2;

    public Edge(Vector2 v1, Vector2 v2)
    {
        this.v1 = v1;
        this.v2 = v2;
    }
};

public class PCGSimple : MonoBehaviour
{
    class Node
    {
        public NodeType type;

        public Vector2 position;
        List<Vector2> pathLocuses;

        List<Path> forks = new List<Path>();

        public Node(Vector2 location)
        {
            this.position = location;
        }
    }

    class Room : Node
    {
        public GameObject room;
        public List<Edge> edges;

        public float width;
        public float height;
        public float area;

        public Room(Vector2 location) : base(location)
        {
            type = NodeType.Room;
            //this.position = location;
        }
    }

    class Corridor : Node
    {
        public List<Segment> segments = new List<Segment>();
        List<Vector2> turnLocuses;

        //public float length;
        //public float width;

        public int numSegments;

        public Corridor(Vector2 location) : base(location)
        {
            //this.position = location;
            type = NodeType.Corridor;
        }
    }

    class Segment
    {
        Corridor parentCorridor;
        public GameObject segment;

        public Vector2 head;
        public Vector2 tail;

        public List<Edge> edges;

        public float width;
        public float length;

        public Segment(Corridor parent)
        {
            parentCorridor = parent;
        }
    }

    class Path
    {
        Node pivot;
        public Vector2 pivotLocus;

        public int numNodes;

        public List<Node> nodes = new List<Node>();

        public Path (Node pivot)
        {
            this.pivot = pivot;
        }
    }

    public const float CONST2D = 0.2f;
    public GameObject spriteTemplate;

    float numRoomMin = 4f;
    float numRoomMax = 12f;

    float roomEdgeMin = 4f;
    float roomEdgeMax = 7.5f;
    float roomAreaMin = 20f;
    float roomAreaMax = 35f;

    //float corridorLenMin = 1f;
    //float corridorLenMax = 15f;
    float corridorNumSegMin = 1f;
    float corridorNumSegMax = 5f;
    float corridorSegLenMin = 1f;
    float corridorSegLenMax = 15f;
    float corridorSegWidMin = 3f;
    float corridorSegWidMax = 4f;

    float ForkLenMin = 1f;
    float ForkLenMax = 3f;

    float CorridorForkProb = 0.5f;
    float RoomForkProb = 0.25f;

    float numRoom;

    private int GenUniformInt(float min, float max)
    {
        float fnum = Random.Range(min, max + 1);
        if (fnum == max + 1)
            fnum = max;

        return Mathf.FloorToInt(fnum);
    }

    private Room GenRoom(Vector2 position, bool correctPos = false)
    {
        Room Room = new Room(position);

        Room.area = Random.Range(roomAreaMin, roomAreaMax);
        Room.width = Random.Range(roomEdgeMin, roomEdgeMax);
        Room.height = Room.area / Room.width;

        if (correctPos)
        {
            Room.position = Room.position + new Vector2(0f, Room.height / 2);
        }

        Vector2 vertex1 = Room.position + new Vector2(-Room.width / 2,  Room.height / 2); // * CONST2D;
        Vector2 vertex2 = Room.position + new Vector2( Room.width / 2,  Room.height / 2); // * CONST2D;
        Vector2 vertex3 = Room.position + new Vector2( Room.width / 2, -Room.height / 2); // * CONST2D;
        Vector2 vertex4 = Room.position + new Vector2(-Room.width / 2, -Room.height / 2); // * CONST2D;

        Edge edge1 = new Edge(vertex1, vertex2);
        Edge edge2 = new Edge(vertex2, vertex3);
        Edge edge3 = new Edge(vertex4, vertex3);
        Edge edge4 = new Edge(vertex1, vertex4);

        Room.edges = new List<Edge> { edge1, edge2, edge3, edge4 };
        Room.room = Instantiate<GameObject>(spriteTemplate, Room.position, Quaternion.identity, gameObject.transform);
        Room.room.name = "Tile";
        Room.room.transform.localScale = new Vector3(Room.width, Room.height, 0f) * CONST2D;

        return Room;
    }

    private Corridor GenCorridor(Edge entranceConstraint, EdgeDirection edgeDirection)
    {
        float width = GenUniformInt(corridorSegLenMin, corridorSegWidMax);
        Vector2 entranceAxis = entranceConstraint.v2 - entranceConstraint.v1;
        Vector2 position = (entranceAxis.magnitude / 2) * entranceAxis.normalized;

        Corridor Corridor = new Corridor(position);
        Corridor.numSegments = GenUniformInt(corridorNumSegMin, corridorNumSegMax);

        for (int i = 0; i < Corridor.numSegments; i++)
        {
            Segment segment = GenSegment(position, edgeDirection, Corridor, width);
            Corridor.segments.Add(segment);
        }

        return Corridor;
    }

    private Segment GenSegment (Vector2 head, EdgeDirection edgeDirection, Corridor parentCorridor, float width)
    {
        Segment segment = new Segment(parentCorridor);

        segment.length = Random.Range(corridorSegLenMin, corridorSegLenMax);
        segment.width  = width;

        segment.head = head;
        segment.tail = Enum2Vector(edgeDirection) * segment.length + head;

        Vector2 vertex1 = segment.head + new Vector2(-segment.width / 2, 0f); // * CONST2D;
        Vector2 vertex2 = segment.head + new Vector2( segment.width / 2, 0f); // * CONST2D;
        Vector2 vertex3 = segment.head + new Vector2( segment.width / 2, -segment.length); // * CONST2D;
        Vector2 vertex4 = segment.head + new Vector2(-segment.width / 2, -segment.length); // * CONST2D;

        Edge edge1 = new Edge(vertex1, vertex2);
        Edge edge2 = new Edge(vertex2, vertex3);
        Edge edge3 = new Edge(vertex4, vertex3);
        Edge edge4 = new Edge(vertex1, vertex4);

        segment.edges = new List<Edge> { edge1, edge2, edge3, edge4 };
        segment.segment = Instantiate<GameObject>(spriteTemplate, segment.head + new Vector2(0f, -segment.length/2),
                                                  Quaternion.identity, gameObject.transform);
        segment.segment.name = "Tile";
        segment.segment.transform.localScale = new Vector3(segment.width, segment.length, 0f) * CONST2D;

        return segment;
    }

    private Vector2 Enum2Vector(EdgeDirection directionTag)
    {
        Vector2 direction = Vector2.zero;

        switch (directionTag)
        {
            case EdgeDirection.Top:
                direction = Vector2.up;
                break;

            case EdgeDirection.Right:
                direction = Vector2.right;
                break;

            case EdgeDirection.Bottom:
                direction = Vector2.down;
                break;

            case EdgeDirection.Left:
                direction = Vector2.left;
                break;
        }

        return direction;
    }

    private Path GenPath(Node pivotNode, EdgeDirection pivotDirection)
    {
        Path Path = new Path(pivotNode);

        Path.numNodes   = GenUniformInt(numRoomMin, numRoomMax) * 2; // There is a corridor for every room
        //Path.pivotLocus = PointPivotLocus(pivotNode, EdgeDirection.Bottom, 5); // MUST SET VARIABLE FOR FORK WIDTH
        //Vector2 lastPivot = Path.pivotLocus;
        Room lastRoom = pivotNode as Room;

        for (int i = 0; i < Path.numNodes; i++)
        {
            Corridor corridor = GenCorridor(lastRoom.edges[(int)EdgeDirection.Bottom + 1], EdgeDirection.Bottom);
            Path.nodes.Add(corridor);

            Room room = GenRoom(corridor.segments[0].tail);
            Path.nodes.Add(room);

            lastRoom = room;
        }

        return Path;
    }

    private Vector2 PointPivotLocus (Node node, EdgeDirection pivotDirection, float forkWidth)
    {
        Edge edge;

        if (node.type == NodeType.Room)
        {
            Room roomNode = node as Room;
            edge = roomNode.edges[(int)pivotDirection];
        }

        else //if (node.type == NodeType.Corridor)
        {
            // MUDAR DEPOIS PARA CORRIDOR
            Corridor corridorNode = node as Corridor;
            edge = corridorNode.segments[0].edges[(int)pivotDirection];
        }

        //Room room = new Room(node.position);

        //Edge edge = room.edges[(int)pivotDirection];

        float smallCoord, largeCoord;

        float p1, p2;
        int constAxis, varAxis;

        if (edge.v1.x == edge.v2.x) {
            p1 = edge.v1.x;
            p2 = edge.v2.x;

            constAxis = 0;
            varAxis   = 1;
        }
        else { //if (edge.v1.y == edge.v2.y) 
            p1 = edge.v1.y;
            p2 = edge.v2.y;

            constAxis = 1;
            varAxis   = 0;
        }

        if (p1 < p2) {
            smallCoord = p1;
            largeCoord = p2;
        }
        else {
            smallCoord = p2;
            largeCoord = p1;
        }

        float locusVarAxis   = Random.Range(smallCoord + forkWidth / 2, largeCoord - forkWidth / 2);
        float locusConstAxis = edge.v1[constAxis];

        Vector2 pivotLocus    = new Vector2();
        pivotLocus[constAxis] = locusConstAxis;
        pivotLocus[varAxis]   = locusVarAxis;

        return pivotLocus;
    }

    // Use this for initialization
    void Start()
    {
        numRoom = GenUniformInt(numRoomMin, numRoomMax);

        Room mainRoom = GenRoom(Vector2.zero);
        Path mainPath = GenPath(mainRoom, EdgeDirection.Bottom);



    }

    // Update is called once per frame
    void Update()
    {

    }
}

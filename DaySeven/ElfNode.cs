using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySeven
{
    public abstract class ElfNode
    {
        public ElfFolder? Parent { get; }
        public string Name { get; }

        public ElfNode(ElfFolder? parent, string name)
        {
            this.Parent = parent;
            this.Name = name;
        }
        public abstract void PrintSelf(int depth = 0);

    }

    public class ElfFile: ElfNode
    {
        public int Size { get; }

        public ElfFile(ElfFolder parent, string name, int size): base (parent, name)
        {
            this.Size = size;
        }

        public override void PrintSelf(int depth = 0)
        {
            for (int i = 0; i < depth; i++) { Console.Write(" "); }
            Console.WriteLine($"- {Name} (file, size={Size})");
        }
    }

    public class ElfFolder : ElfNode
    {
        public List<ElfNode> Children { get; } = new List<ElfNode>();
        public ElfFolder(ElfFolder? parent, string name) : base(parent, name) { }
        public int TotalSize { get; set; } = 0;

        public bool SmallFolder = true;

        public bool ContainsChildWith(string name)
        {
            return Children.Any(child => child.Name == name);
        }
        public ElfNode GetFolder(string name)
        {
            return Children.First(child => child.Name == name);
        }

        public void UpdateSize(int addition)
        {
            TotalSize += addition;
            if (TotalSize >= 100000) SmallFolder = false;
            if (Parent != null) Parent.UpdateSize(addition);
        }

        public void AddChild (ElfNode child)
        {
            if (ContainsChildWith(child.Name))
            {
                Console.Write($"Folder {Name} already has a child with path {child.Name}");
            }
            else
            {
                if (child.GetType() == typeof(ElfFile)) { UpdateSize(((ElfFile)child).Size); }
                Children.Add(child);
            }
        }

        public List<ElfFolder> GetSmallChildren()
        {
            List<ElfFolder> smallFolders = new List<ElfFolder>();
            foreach (var child in Children)
            {
                if (child.GetType() == typeof(ElfFile)) continue;
                var folder = (ElfFolder)child;
                if (folder.SmallFolder) smallFolders.Add(folder);
                var childsSmallFolders = folder.GetSmallChildren();
                smallFolders.AddRange(childsSmallFolders);
            }
            return smallFolders;
            }

        public List<ElfFolder> GetAllChildren()
        {
            List<ElfFolder> allFolders = new List<ElfFolder>();
            foreach (var child in Children)
            {
                if (child.GetType() == typeof(ElfFile)) continue;
                var folder = (ElfFolder)child;
                allFolders.Add(folder);
                var childsFolders = folder.GetAllChildren();
                allFolders.AddRange(childsFolders);
            }
            return allFolders;
        }

        public override void PrintSelf(int depth = 0)
        {
            for (int i = 0; i < depth; i++) { Console.Write(" "); }
            if (SmallFolder) Console.WriteLine($"- {Name} (dir, GOLDEN!, TotalSize={TotalSize})");
            else Console.WriteLine($"- {Name} (dir, TotalSize={TotalSize})");
            foreach (var child in Children)
            {
                child.PrintSelf(depth+2);
            }
        }
    }
}

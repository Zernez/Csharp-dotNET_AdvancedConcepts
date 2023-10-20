using System.Diagnostics;
using static System.Console;

Task outerTask = Task.Factory.StartNew(OuterMethod);
outerTask.Wait();
Task innerTask = Task.Factory.StartNew(InnerMethod, TaskCreationOptions.AttachedToParent);
WriteLine("Console app is stopping.");

static void OuterMethod() { 
    WriteLine("Outer method starting..."); 
    Task innerTask = Task.Factory.StartNew(InnerMethod); WriteLine("Outer method finished."); 
}
static void InnerMethod() { 
    WriteLine("Inner method starting..."); 
    Thread.Sleep(2000); 
    WriteLine("Inner method finished."); 
}


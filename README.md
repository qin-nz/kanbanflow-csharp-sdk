# KanbanFlow C# SDK
A third party SDK of https://kanbanflow.com

# Instructions
First, you need a apiToken for your board. Each board need a independent apiToken.  
You can get your apiToken from https://kanbanflow.com/administration ,then click `Board Settings`, `API`.  

```csharp
 KanbanFlowClient client = new KanbanFlowClient(<your-apiToken-for-a-board>);
 //get a board
 var board = client.GetBoardAsync();
```
 
We can create a task by using following code.
```csharp
var newTask= await board.CreateTaskAsync(new Task
            {
                Name = "Your Task Name",
                Description = "Description,you can use Chinese character.",
				
				//Set task position. We now set it at first column (by default,it's "To Do" column).
                ColumnId = _board.Cells[0].ColumnId,
				//Set task position. If you not enable Swimlane,set it to null
                SwimlaneId = _board.Cells[0].SwimlaneId,
            });
```

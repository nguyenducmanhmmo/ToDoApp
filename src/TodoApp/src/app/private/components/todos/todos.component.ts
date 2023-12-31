import { Component, OnInit, Input } from '@angular/core';
import { TodoItems } from 'src/shared/todo-items.model';
import { TodoItemService } from 'src/shared/todo-items.service';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css']
})
export class TodosComponent implements OnInit {

  //component props to pass 
  title = "todo-items";
  id = "todo-list";
  editDisplay = false;
  showEditButton = true;

  //imports todo class as a Type
  todos: TodoItems[] = [];
  todo: any;
  todoName: any = "404";
  randomData: any = {};
  inputTodo: string = "";
  inputTodoSecret: string = "";
  inputTodoPriority: number = 1;
  edittodo: any;

  constructor(private api: TodoItemService) {  }

  //on component mount
  ngOnInit(): void {
    this.api.refreshList().subscribe((data: any) =>  {
      this.todos = data.map((res:any) => ({ TodoName: res.title, id : res.id}));
    });
  }

  // Toggles a todo to the completed state
  toggleDone(id: number) {
    this.todos.map((value, i) => {
      //validate id of todo item
      if (i === id) {
        if (value.IsComplete === "true") {
          value.IsComplete = "false";
        } else {
          value.IsComplete = "true";
        }
      }
    });
  }

  //opens edit component
  editTodo(todo : any) {
    this.editDisplay = !this.editDisplay;
    this.editTodo = todo;
  }

  // Deletes an item from the list
  deleteTodo(id: number) {
    this.todos = this.todos.filter((value, i) => i !== id);
  }

  // Deletes all todos from list
  deleteAll() {
    this.editDisplay = false;
    this.todos = [];
  }

  // Pushes a new todo to state and api, clears form
  addTodo() {
    this.showEditButton = true;
    if (this.inputTodo === "") {
      return null;
    } else {
      this.todo = { TodoName: this.inputTodo }
      this.api.addTodo(this.todo).subscribe((data: any) => {this.todos.push({
        Id: data.id, //Id is indexed by SqlClient table
        TodoName: data.title,
        IsComplete: "false",
        TodoPriority: this.inputTodoPriority
      });});
      //clear input after submit
      this.inputTodo = "";
      return "pushed to list";
    }
  }

  closeSubmit() {
    this.showEditButton = false;
    this.editDisplay = !this.editDisplay;
  }
}
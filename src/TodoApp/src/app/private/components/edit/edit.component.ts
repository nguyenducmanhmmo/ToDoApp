import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TodoItems } from 'src/shared/todo-items.model';
import { TodoItemService } from 'src/shared/todo-items.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  //Recieve props with @Input()
  @Input() display = false; 
  @Input() editTodo: any; 
  @Input() todos: TodoItems[] = [];
  //Pass back props with @Output()
  @Output() hideSubmit = new EventEmitter<boolean>();
  //One-way bound
  inputTodo: string = "";
  todoSecret: string = "";
  secret: boolean = false;
  todo: any;

  constructor(private api: TodoItemService) { }

  ngOnInit(): void { 
  }

  //Call put request and edit a todo item
  editTodoApi(inputTodoEdit: any) {
    console.log(inputTodoEdit);
    console.log(this.editTodo);
    this.hideSubmit.emit(!this.hideSubmit);
    if (this.inputTodo !== "") {
      let todo = { "Title": inputTodoEdit};
      this.api.updateTodo(this.editTodo.id, todo).subscribe((data: any) => {       
      });
      this.todos.map((item : any) => {
        if (item.id === this.editTodo.id) {
          item.TodoName = inputTodoEdit;
        }
      });
    } else {
      alert("Please provide a todo value");
    }
  }
}
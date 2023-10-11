import { Injectable } from "@angular/core";
import { TodoItems } from "./todo-items.model";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment.prod";

@Injectable({
    providedIn: 'root'
  })
  export class TodoItemService {
  
    formData: TodoItems = new TodoItems();
    readonly APIUrl = environment.apiUrl;
    list: any;
    
    constructor(private http: HttpClient) { }
  
    getTodoList(): Observable<any[]> {
      return this.http.get<any>(this.APIUrl + '/todo');
    }
  
    addTodo(val: any) {
      return this.http.post(this.APIUrl + '/todo', {title : val.TodoName});
    }
  
    updateTodo(id: any, val: any) {
      return this.http.put(this.APIUrl + `/todo/${id}`, val);
    }
  
    deleteTodo(id: any) {
      return this.http.delete(this.APIUrl + `/todo/${id}`);
    }
  
    //Populates existing records into list property.
    refreshList() {
      return this.http.get(this.APIUrl + '/todo');
    }
  
    //Random api call (this one is not using async/await)
    apiCall() {
      return this.http.get(this.APIUrl + '/api/todo');
    }
  }
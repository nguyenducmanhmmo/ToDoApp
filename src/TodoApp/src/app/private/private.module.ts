import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PrivateRoutingModule} from "./private-routing.module";
import { TodosComponent } from './components/todos/todos.component';
import { FormsModule } from '@angular/forms';
import { EditComponent } from './components/edit/edit.component';

@NgModule({
  declarations: [
    TodosComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    PrivateRoutingModule,
    FormsModule
  ]
})
export class PrivateModule { }
import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import { TodosComponent } from "./components/todos/todos.component";
import { AuthGuard } from "../helpers/auth.guard";


export const routes: Routes = [
  {
    path: 'todo',
    component: TodosComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '**',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrivateRoutingModule{}
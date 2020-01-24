import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { HttpClientModule } from '@angular/common/http'
import { HashLocationStrategy, LocationStrategy } from '@angular/common';

import { AppComponent } from './app.component';
import { ProjectComponent } from './_components/project/project.component';
import { RouterModule, Routes } from '@angular/router';
import { NavComponent } from './_components/nav/nav/nav.component';
import { TaskComponent } from './_components/task/task.component';
import { UserComponent } from './_components/user/user.component';
import { ViewtaskComponent } from './_components/viewtask/viewtask.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import {AgGridModule} from 'ag-grid-angular/main';
import { SelectDropDownModule } from 'ngx-select-dropdown'




const routes: Routes = [
  { path: '', component: ProjectComponent },
  { path: 'projects', component: ProjectComponent },
  { path: 'tasks', component: TaskComponent },
   { path: 'tasks/:id', component: TaskComponent },
  { path: 'users', component: UserComponent },
  { path: 'viewtask', component: ViewtaskComponent }

];

@NgModule({
  declarations: [
    AppComponent,
    ProjectComponent,
    NavComponent,
    TaskComponent,
    UserComponent,
    ViewtaskComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    Ng2SearchPipeModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    AgGridModule.forRoot(),
    RouterModule.forRoot(routes),
    SelectDropDownModule
    
  ],
  providers: [{provide: LocationStrategy, useClass: HashLocationStrategy}],
  bootstrap: [AppComponent],
    exports: [BsDropdownModule, TooltipModule, ModalModule]

})
export class AppModule { }

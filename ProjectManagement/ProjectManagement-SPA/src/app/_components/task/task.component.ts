import { Component, OnInit, NgProbeToken, TemplateRef } from '@angular/core';
import { Form, FormGroup } from '@angular/forms';
import { Router,ActivatedRoute,ParamMap } from '@angular/router';
import { TaskService } from 'src/app/_services/task.service';
import { TaskData } from 'src/app/_models/task.data';
import { ProjectService } from 'src/app/_services/project.service';
import { ProjectData } from 'src/app/_models/project.data';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserService } from 'src/app/_services/user.service';
import { UserData } from 'src/app/_models/user.data';
import { ParentTaskData } from 'src/app/_models/parenttask.data';
import { switchMap } from 'rxjs/operators';
@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {
  private title: string = 'Add Task';
 private  SubmitBtnName: string = 'Add Task';
  private project_ID: number;
  private projectname: string;
  private user_ID: string;
  private username: string; 
  private parenttask: string;
  private parentid: number;
  private isChecked:boolean;
    private todate = new Date();
  tasks: TaskData[]=[];
  projects: ProjectData[];
  parenttasks: ParentTaskData[]; 
  isEdit: boolean;
  taskId: number;
  taskData: TaskData = { Task_ID: null, Parent_ID: null, Project_ID: null, TaskName: '', Start_Date:  new Date(), End_Date: this.todate, Priority: 0, Status: '', Users:null, ParentTask:null,User_ID:null }
  parenttaskData: ParentTaskData = { Parent_ID: null, Parent_Task: '' }
  projectData: ProjectData = { Project_ID: null, ProjectName: '', StartDate: null, EndDate:  new Date(), Priority: null, User_ID: null, Users: null,Tasks:null }
  users: UserData[];
  userData: UserData = { User_ID: null, FirstName: '', LastName: '', Employee_ID: null, Project_ID: null, Task_ID: null }

  constructor(private modalService: BsModalService, private userService: UserService, private projectService: ProjectService, private router: Router, private taskService: TaskService,
   private route: ActivatedRoute) { } // {2}
  ngOnInit() {
      let id = this.route.snapshot.paramMap.get('id');
      if (id != null){
      this.edit(parseInt(id));

    }
 
    this.taskService.getparentTasks()
      .subscribe(response => {
        this.parenttasks = response;
        if(this.taskId != null && this.taskId !=0 && this.parentid != null && this.parentid !=0)
        this.parenttask = this.parenttasks.filter(x=> x.Parent_ID == this.parentid)[0].Parent_Task;
      })
    this.projectService.getProjects()
      .subscribe(response => {
        this.projects = response;
        if(this.taskId != null && this.taskId !=0 && this.project_ID != null && this.project_ID !=0)
        this.projectname = this.projects.filter(x=> x.Project_ID == this.project_ID)[0].ProjectName;
      })
    this.userService.getUsers()
      .subscribe(response => {
        this.users = response;
        if(this.taskId != null && this.taskId !=0 && this.user_ID != null && this.user_ID !='')
        this.username = this.users.filter(x=> x.User_ID.toString() == this.user_ID)[0].FirstName +' '+ this.users.filter(x=> x.User_ID.toString() == this.user_ID)[0].LastName ;
      });
        this.todate.setDate( this.todate.getDate() + 1 );
  }
  public modalRef: BsModalRef; // {1}

  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);// {3}
  }

  public closeModal(template: TemplateRef<any>) {
    // this.userBodyTextname=template.elementRef.
    this.modalService.hide(1);
    //search(); 
  }
  onChange(event: any) {
    //console.log(event);
    this.projectname = event.target.options[event.target.options.selectedIndex].text;
    this.project_ID = event.target.options[event.target.options.selectedIndex].value;
    // console.log(text);

  }
  onUserChange(event: any) {
    //console.log(event);
    this.username = event.target.options[event.target.options.selectedIndex].text;
    this.user_ID = event.target.options[event.target.options.selectedIndex].value;
    // console.log(text);
  }
  ontaskChange(event: any) {
    //console.log(event);
     this.parenttask = event.value[0].Parent_Task;
     this.parentid = event.value[0].Parent_ID;
    // console.log(Parent_IDtext);Parent_ID

  }
  submit(form: FormGroup) {
    const request = form.value;
    if (this.user_ID!=null){
    request.user_ID = this.user_ID.toString();
    }
    request.Project_ID = this.project_ID.toString();
    if (this.parentid!=null){
    request.Parent_ID = this.parentid.toString();
    }
    if (!this.isEdit) {
      this.taskService.saveTask(request)
        .subscribe(response => {
          this.tasks.push(response);
          confirm('Task Details Saved Successfully');
        }, error => {
          for (const key of Object.keys(error.error.ModelState)) {
            for (const err of error.error.ModelState[key]) {
              alert(err);
            }
          }
        }, () => {
          form.reset();

        })
    } else {
      request.Task_ID = this.taskId;
      this.taskService.saveTask(request)
        .subscribe(task => {
          var tskIndex = this.tasks.findIndex(tsk => tsk.Task_ID == this.taskId);

          if (tskIndex != -1) {
            this.tasks[tskIndex] = task;
          }
          confirm('Task Details Updated Successfully');
          this.router.navigate(['/viewtask']);
        }, error => {
          for (const key of Object.keys(error.error.ModelState)) {
            for (const err of error.error.ModelState[key]) {
              alert(err);
            }
          }
        }, () => {
          form.reset();
          this.isEdit = false;
        })
    }
  }

  delete(id: number) {
    let isDelete = confirm('Do you wish to delete the Task?')

    if (isDelete) {
      this.taskService.deleteTask(id)
        .subscribe(response => {
          if (response) {
            const projIndex = this.tasks.findIndex(proj => proj.Task_ID == id);
            if (projIndex != -1) {
              this.tasks.splice(projIndex, 1);
            }
          }
        });
    }
  }

  edit(id: number) {
this.title = 'Update Task';
this.taskId = id;
  this.SubmitBtnName = 'Update';
    this.taskService.getTask(id)
      .subscribe(task => {
        this.taskData.Parent_ID = task.Parent_ID;
        this.taskData.Project_ID = task.Project_ID;
        this.taskData.TaskName = task.TaskName;
        this.taskData.End_Date = task.End_Date;
        this.taskData.Start_Date = task.Start_Date;
        this.taskData.Priority = task.Priority;
        this.taskData.Status = task.Status;
        this.taskData.User_ID = task.User_ID;
        this.project_ID = task.Project_ID;
        this.parentid = task.Parent_ID;
        
        this.user_ID = task.User_ID.toString();
        this.isEdit = true;
        this.taskId = id;
        
      })
  }

  view(id: number) {
    this.router.navigate(['/tasks', id]);
  }

  config = {
            displayKey:"Parent_Task" ,//if objects array passed which key to be displayed defaults to description,
            search:true//enables the search plugin to search in the list
          }
          
    singleSelect: any = [];      

}

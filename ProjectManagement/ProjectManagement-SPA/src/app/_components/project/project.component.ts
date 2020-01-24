import { Component, OnInit, NgProbeToken, TemplateRef } from '@angular/core';
import { Form, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ProjectService } from 'src/app/_services/project.service';
import { ProjectData } from 'src/app/_models/project.data';
import { UserService } from 'src/app/_services/user.service';
import { UserData } from 'src/app/_models/user.data';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {
  private user_ID: number;
  private username: string;
    private todate = new Date();
  projects: ProjectData[]; 
  isEdit: boolean;
  projectId: number;
  projectData: ProjectData = { Project_ID: null, ProjectName: '', StartDate:  new Date(), EndDate: this.todate, Priority: 0, User_ID: null, Users: null,Tasks:null }
  users: UserData[];
  userData: UserData = { User_ID: null, FirstName: '', LastName: '', Employee_ID: null, Project_ID: null, Task_ID: null }

  ngOnInit() {
    this.projectService.getProjects()
      .subscribe(response => {
        this.projects = response;
        console.log(response);
        
      })
    this.userService.getUsers()
      .subscribe(response => {
        this.users = response;
        console.log(response);
      });
this.todate.setDate( this.todate.getDate() + 1 );
    
  }
  public modalRef: BsModalRef; // {1}
  constructor(private modalService: BsModalService, private userService: UserService, private projectService: ProjectService, private router: Router) { } // {2}


  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);// {3}
  }

  isDisabled = true;
  triggerEnableDate() {
      this.isDisabled = !this.isDisabled;
      return;
  }

  onChange(event: any) {
    this.username = event.target.options[event.target.options.selectedIndex].text;
    this.user_ID = event.target.options[event.target.options.selectedIndex].value;
  }
  submit(form: FormGroup) {
    const request = form.value;
    request.user_ID = this.user_ID.toString();
    if (!this.endDateAfterOrEqualValidator(form))
      alert('Project End date should be greater than Start date.');
    else if (!this.isEdit) {
      this.projectService.saveProject(request)
        .subscribe(response => {
          this.projects.push(response);
          confirm('Project Details Saved Successfully');
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
      request.Project_ID = this.projectId;
      this.projectService.saveProject(request)
        .subscribe(project => {
          var projIndex = this.projects.findIndex(proj => proj.Project_ID == this.projectId);

          if (projIndex != -1) {
            this.projects[projIndex] = project;
          }
          confirm('Project Details Updated Successfully');
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
    let isDelete = confirm('Do you wish to delete the Project?')

    if (isDelete) {
      this.projectService.deleteProject(id)
        .subscribe(response => {
          if (response) {
            const projIndex = this.projects.findIndex(proj => proj.Project_ID == id);
            if (projIndex != -1) {
              this.projects.splice(projIndex, 1);
            }
          }
        });
    }
  }

  edit(id: number) {
    this.projectService.getProject(id)
      .subscribe(project => {

        this.projectData.ProjectName = project.ProjectName;
        this.projectData.StartDate = project.StartDate;
        this.projectData.EndDate = project.EndDate;
        this.projectData.Priority = project.Priority;
        this.projectData.User_ID = project.User_ID;
        this.user_ID = project.User_ID;
        console.log(project.User_ID);
        this.username = this.users.find(i => i.User_ID == project.User_ID).FirstName + '  ' + this.users.find(i => i.User_ID == project.User_ID).LastName;
        this.isEdit = true;
        this.projectId = id;
      })
  }

  reset()
  {
    this.isDisabled = true;
  }

  sort(param: any) {
    if (param == 'StartDate') {
      this.projectService.getProjects()
        .subscribe(response => {
          this.projects = response.sort((a, b) => {
            return <any>new Date(b.StartDate) - <any>new Date(a.StartDate);
          });
        })
    }
    if (param == 'EndDate') {
      this.projectService.getProjects()
        .subscribe(response => {
          this.projects = response.sort((a, b) => {
            return <any>new Date(b.StartDate) - <any>new Date(a.StartDate);
          });
        })
    }
    if (param == 'Priority') {
      this.projectService.getProjects()
        .subscribe(response => {
          this.projects = response.sort((a, b) => a.Priority.toString().localeCompare(b.Priority.toString(), undefined, { numeric: true }));
        });
    }
      if (param == 'Completed') {
      this.projectService.getProjects()
         .subscribe(response => {
          this.projects = response.sort((a, b) => a.Priority.toString().localeCompare(b.Priority.toString(), undefined, { numeric: true }));
        }); 
      }
  }
  endDateAfterOrEqualValidator(formGroup:FormGroup){
    var startDateTimestamp, endDateTimestamp;
    for (var controlName in formGroup.controls) {
      if (controlName.indexOf("startdate") !== -1) {
        startDateTimestamp = Date.parse(formGroup.controls[controlName].value);
      }
      if (controlName.indexOf("enddate") !== -1) {
        endDateTimestamp = Date.parse(formGroup.controls[controlName].value);
      }
    }
    return (endDateTimestamp < startDateTimestamp) ? false : true;
  }
  compareTwoDates(form:FormGroup){
   if(new Date(form.controls['StartDate'].value)<new Date(form.controls['EndDate'].value)){
      return false;
   }
}
  view(id: number) {
    this.router.navigate(['/projects', id]);
  }

}
import { Component, OnInit, NgProbeToken, TemplateRef } from '@angular/core';
import { Form, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';
import { UserData } from 'src/app/_models/user.data';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';



@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  private user_ID: string;
  private username: string;

  users: UserData[];
  isEdit: boolean;
  userId: number;
  userData: UserData = { User_ID: null, FirstName: '', LastName: '', Employee_ID: null, Project_ID: null, Task_ID: null }


  ngOnInit() {
    this.userService.getUsers()
      .subscribe(response => {
        this.users = response;
        console.log(response);
      });

    this.user_ID = "";
   
  }
  public modalRef: BsModalRef; // {1}
  constructor(private modalService: BsModalService, private userService: UserService, private router: Router) { } // {2}


  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);// {3}
  }

  public closeModal(template: TemplateRef<any>) {
    this.modalService.hide(1);
  }
  
  submit(form: FormGroup) {
    const request = form.value;

    if (!this.isEdit) {
      this.userService.saveUser(request)
        .subscribe(response => {
          this.users.push(response);
confirm('User Details Updated Successfully');
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
      request.User_ID = this.userId;
      this.userService.saveUser(request)
        .subscribe(user => {
          var userIndex = this.users.findIndex(usr => usr.User_ID == this.userId);
          if (userIndex != -1) {
            console.log(user);
            this.users[userIndex] = user;
          }
          confirm('User Details Updated Successfully');
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
    let isDelete = confirm('Do you wish to delete the User?')

    if (isDelete) {
      this.userService.deleteUser(id)
        .subscribe(response => {
          if (response) {
            const userIndex = this.users.findIndex(user => user.User_ID == id);
            if (userIndex != -1) {
              this.users.splice(userIndex, 1);
            }
          }
        });
    }
  }

  edit(id: number) {
    this.userService.getUser(id)
      .subscribe(user => {
        this.userData.FirstName = user.FirstName;
        this.userData.LastName = user.LastName;
        this.userData.Employee_ID = user.Employee_ID;
        this.userData.Project_ID = user.Project_ID;
        this.userData.Task_ID = user.Task_ID;
        this.isEdit = true;
        this.userId = id;
      })
  }
  sort(param: any) {
    if (param == 'FirstName') {
      this.userService.getUsers()
        .subscribe(response => {
          this.users = response.sort((a, b) => a.FirstName.localeCompare(b.FirstName));
        })
    }
    if (param == 'LastName') {
      this.userService.getUsers()
        .subscribe(response => {
          this.users = response.sort((a, b) => a.LastName.localeCompare(b.LastName));
        })
    } if (param == 'Id') {
      this.userService.getUsers()
        .subscribe(response => {
          this.users = response.sort((a, b) => a.Employee_ID.toString().localeCompare(b.Employee_ID.toString(), undefined, { numeric: true }));
        })
    }
  }

  		
  view(id: number) {
    this.router.navigate(['/users', id]);
  }

}

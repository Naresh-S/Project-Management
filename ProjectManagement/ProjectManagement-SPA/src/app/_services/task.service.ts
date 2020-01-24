import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { TaskData } from '../_models/task.data';
import { ParentTaskData } from '../_models/parenttask.data';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl: string = 'http://localhost:57022/api/tasks';
  // private apiUrl: string = 'http://localhost/ProjectManagementService/api/tasks';
  constructor(private http: HttpClient) { }

  saveTask(request: any): Observable<TaskData> {
    return this.http.post<TaskData>(this.apiUrl, request);
  }

  getTasks(): Observable<TaskData[]> {
    return this.http.get<TaskData[]>(this.apiUrl);
  }
  getparentTasks(): Observable<ParentTaskData[]> {
    return this.http.get<ParentTaskData[]>(this.apiUrl+"/GetParentTasks");
  }

  deleteTask(id: number): Observable<TaskData> {
    return this.http.get<TaskData>(this.apiUrl + '/DeleteTaskData/' + id);
  }

  getTask(id: number): Observable<TaskData> {
    return this.http.get<TaskData>(this.apiUrl + '/' + id);
  }
}

import { UserData } from 'src/app/_models/user.data';
import {ParentTaskData} from 'src/app/_models/parenttask.data';
export interface TaskData {
    Task_ID: number;
    Parent_ID: number;
    Project_ID: number;
    TaskName: string;
    Start_Date: Date;
    End_Date: Date;
    Priority: number;
    Status: string;
    ParentTask: ParentTaskData;
    Users:UserData;
    User_ID:number;
}


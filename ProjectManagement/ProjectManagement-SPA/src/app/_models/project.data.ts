import { UserData } from 'src/app/_models/user.data';
import { TaskData } from 'src/app/_models/task.data';
export interface ProjectData {
    Project_ID: number;
    ProjectName: string;
    EndDate?: Date;
    StartDate?: Date;
     Priority?: number;
     User_ID:number;
     Users:UserData;
     Tasks:TaskData;
     }

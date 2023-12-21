import { Component, OnInit } from '@angular/core';
import { Dialog } from '@capacitor/dialog';
import { ApiService } from 'src/services/api.service';
import { IonItemSliding } from '@ionic/angular';



@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  incompleteTasks: any[] = [];
  completedTasks: any[] = [];
  slidingItem: IonItemSliding | undefined;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.fetchTasks();
  }


  

  async fetchTasks() {
    try {
      console.log('Fetching tasks...');
      const incompleteTasks = await this.apiService.getIncompleteTasks().toPromise();
      const completedTasks = await this.apiService.getCompletedTasks().toPromise();
  
      console.log('Completed Tasks:', completedTasks); 
  
      if (incompleteTasks !== undefined) {
        this.incompleteTasks = incompleteTasks;
      }
  
      if (completedTasks !== undefined) {
        this.completedTasks = completedTasks;
      }
    } catch (error) {
      console.error('Error fetching tasks:', error);
    }
  }
  
  
   
   async taskDialog() {
    const result = await Dialog.prompt({
      title: 'New Task',
      message: 'Enter a new task name',
    });
  
    if (result.value !== undefined) {
      const newTask = {
        title: result.value,
        completed: false,
      };
  
      try {
        await this.apiService.createTask(newTask).toPromise();
        this.fetchTasks();
      } catch (error) {
        console.error('Error creating task:', error);
      }
    }
   }
 
   async deleteTask(task: any) {
    try {
      await this.apiService.deleteTask(task.taskId).toPromise();
      await this.fetchTasks(); 
      this.slidingItem?.close();
    } catch (error) {
      console.error('Error deleting task:', error);
    }
  }
  

  async toggleTaskCompletion(task: any) {
    if (!task || !task.taskId) {
      console.error('Invalid task object:', task);
      return;
    }
  
    console.log('Toggling task completion for task ID:', task.taskId);
  
    try {
      await this.apiService.toggleTaskCompletion(task.taskId).toPromise();
  
      const taskIndex = this.incompleteTasks.findIndex(t => t.taskId === task.taskId);
  
      if (taskIndex !== -1) {
        const completedTask = this.incompleteTasks.splice(taskIndex, 1)[0];
        completedTask.Completed = true;
        this.completedTasks.push(completedTask);
      } else {
        const incompleteTaskIndex = this.completedTasks.findIndex(t => t.TaskId === task.taskId);
        const incompleteTask = this.completedTasks.splice(incompleteTaskIndex, 1)[0];
        incompleteTask.Completed = false;
        this.incompleteTasks.push(incompleteTask);
      }
  
      
    } catch (error) {
      console.error('Error toggling task completion:', error);
    }
  }
  
  

}

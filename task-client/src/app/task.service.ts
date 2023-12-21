import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private apiUrl = 'http://localhost:5198/api/Tasks';  

  constructor(private http: HttpClient) {}

  getTasks(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/tasks`);
  }

  createTask(newTask: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/tasks`, newTask);
  }

  deleteTask(taskId: number): Observable<any> {
    
    return this.http.delete<any>(`${this.apiUrl}/tasks/${taskId}`);
  }

  
}

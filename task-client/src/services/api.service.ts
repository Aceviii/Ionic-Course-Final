import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiUrl = 'http://localhost:5198/api/Tasks'; 

  constructor(private http: HttpClient) {}

  getIncompleteTasks(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/incomplete-tasks`)
      .pipe(
        catchError(this.handleError)
      );
  }

  getCompletedTasks(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/completed-tasks`)
      .pipe(
        catchError(this.handleError)
      );
  }

 
createTask(newTask: any): Observable<any> {
  return this.http.post('http://localhost:5198/api/Tasks', newTask);
}

toggleTaskCompletion(taskId: number): Observable<any> {
  const url = `http://localhost:5198/api/Tasks/${taskId}/toggle-completion`;
  return this.http.patch<any>(url, null)
    .pipe(
      catchError(this.handleError)
    );
}



  deleteTask(taskId: number): Observable<any> {
    const url = `http://localhost:5198/api/Tasks/${taskId}`;
    return this.http.delete(url);
  }
  

  private handleError(error: HttpErrorResponse) {
    console.error('API Request Error:', error);
  
    if (error.error instanceof ErrorEvent) {
      
      console.error('Client-side error:', error.error.message);
    } else {
      
      console.error(`Server-side error (status ${error.status}):`, error.error);
    }
  
    
    return throwError('Something went wrong. Please try again later.');
  }
}

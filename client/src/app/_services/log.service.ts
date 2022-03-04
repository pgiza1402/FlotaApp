import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Log } from '../_models/log';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  baseUrl = environment.apiUrl
  constructor(private http: HttpClient) { }


  getLogs(){
    return this.http.get<Log[]>(this.baseUrl + "log");
  }
  
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl
  
  constructor(private http: HttpClient) { }


getUsers(){
  return this.http.get<Partial<User[]>>(this.baseUrl + 'user')
}

addUser(user: User){
  return this.http.post(this.baseUrl + 'user/addUser', user);
}

getUserById(id: number){
  return this.http.get<User>(this.baseUrl + 'user/' + id)
}

updateUser(user: User, id: number){
  return this.http.put(this.baseUrl + 'user/' + id, user)
}

checkUsernameExists(userName: string){

  return this.http.get(this.baseUrl + 'user/usernametaken?userName=' + userName)
}
}

 
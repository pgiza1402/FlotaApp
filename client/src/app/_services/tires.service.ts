import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Tires } from '../_models/tires';
import { TiresForList } from '../_models/tiresForList';



@Injectable({
  providedIn: 'root'
})
export class TiresService {
  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }


  addTires(tires: Tires){
    return this.http.post(this.baseUrl + 'tires', tires);

  }

  getTires(){
    return this.http.get<TiresForList[]>(this.baseUrl + 'tires');
  }

  updateTires(tires: Tires, id: number){
  return this.http.put(this.baseUrl + 'tires/' + id, tires)
  }

  getTiresById(id: number){
    return this.http.get<Tires>(this.baseUrl + 'tires/' + id)
  }
}

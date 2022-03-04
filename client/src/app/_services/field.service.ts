import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Field } from '../_models/field';
import { FieldForList } from '../_models/fieldForList';

@Injectable({
  providedIn: 'root'
})
export class FieldService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addField(field: Field){
    return this.http.post(this.baseUrl + 'field' , field);
}

  getFields(){
    return this.http.get<FieldForList[]>(this.baseUrl + 'field');
  }

  getFieldById(id: number){
    return this.http.get<FieldForList>(this.baseUrl + 'field/' + id);

  }
  deleteField(id: Number){
    return this.http.delete(this.baseUrl + 'field/' + id);
  }

  updateField(field: Field, id: number){
    return this.http.put<Field>(this.baseUrl + 'field/' + id, field);
  }


}

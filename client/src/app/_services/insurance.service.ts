import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Insurance } from '../_models/insurance';
import { InsuranceForList } from '../_models/insuranceForList';


@Injectable({
  providedIn: 'root'
})
export class InsuranceService {
  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }


  getInsurances(){
    return this.http.get<InsuranceForList[]>(this.baseUrl + 'insurance')

  }

  getInsuranceById(id: number){
    return this.http.get<InsuranceForList>(this.baseUrl + 'insurance/' + id)
  }

  updateInsurance(insurance: Insurance, id: number){
    return this.http.put(this.baseUrl + 'insurance/' + id, insurance);
  }

  // addInsurance(insurance: Insurance){
  //   return this.http.post(this.baseUrl + 'insurance', insurance);
  // }
}

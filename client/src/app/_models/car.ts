import { Insurance } from "./insurance";
import { Service } from "./service";

export interface Car {
    brand: string;
    model: string;
    userName: string;
    year : number;
    registrationNumber: string;
    meterStatus: string;
    vat: string;
    isArchival: boolean;
    technicalExaminationExpirationDate: Date;
    insurance: Insurance
    service: Service
}
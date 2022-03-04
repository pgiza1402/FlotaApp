export interface CarForList {
    id: number;
    brand: string;
    model: string;
    userName: string;
    registrationNumber: string;
    meterStatus: string;
    vat: string;
    carInsuranceExpirationDate: Date;
    technicalExaminationExpirationDate: Date;
    serviceExpirationDate: Date;
    nextServiceMeterStatus: number;
    year: number;
    isArchival: boolean;
}
import { IncidentType } from './incidenttype';


export class Incident{
    id: number;
    incidentDate: Date;
    incidentTime: Date;
    description: string;
    person: string;
    incidentTypeId: number
}
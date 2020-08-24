import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ClientModel } from './clients/client.model';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  constructor(private http: HttpClient) {}

  listarClientes() : Observable<any> {
    return this.http.get('http://localhost:49493/api/clients/');
  }

  cadastrarCliente(client: ClientModel) : Observable<any> {
    return this.http.post("http://localhost:49493/api/clients/", client);
  }
}

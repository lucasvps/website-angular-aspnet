import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ClientModel } from '../clients/client.model';
import { ClientsService } from '../../clients.service';


@Component({
	selector: 'app-cadastro-cliente',
	templateUrl: './cadastro-cliente.component.html',
	styleUrls: ['./cadastro-cliente.component.css']
})
export class CadastroClienteComponent implements OnInit {

	cliente: ClientModel = new ClientModel();

	form: FormGroup;

	erro: string;

	httpOptions = {
		headers: new HttpHeaders({
			'Content-Type': 'application/json',
			'Accept': 'application/json',
		})
	};

	public noWhitespaceValidator(control: FormGroup) {
		const isWhitespace = (control.value || '').trim().length === 0;
		const isValid = !isWhitespace;
		return isValid ? null : { 'whitespace': true };
	}

	constructor(private formBuilder: FormBuilder, private clientService: ClientsService, private http: HttpClient, private router: Router) {
		this.form = this.formBuilder.group({
			nome:['', [Validators.minLength(3), Validators.maxLength(30), this.noWhitespaceValidator]],
			email: ['', Validators.email],

		});
	}

	ngOnInit() {
	}

	cadastrar() {
		if (this.form.invalid) {
			return;
		}

		this.cliente.Name = this.form.get('nome').value;
		this.cliente.Email = this.form.get('email').value;

		//console.log(this.client);
		this.clientService.cadastrarCliente(this.cliente).subscribe(res => {
			this.router.navigate(['clientes']);
		}, err => {
			this.erro = err.erro.Message;
			
		})
	}

	home(){
		this.router.navigate(['home']);
	  }
}

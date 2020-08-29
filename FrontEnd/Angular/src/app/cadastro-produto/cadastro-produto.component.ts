import { Component, OnInit } from '@angular/core';
import { ProdutoModel } from '../produtos/produto.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProdutoService } from '../../produto.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
	selector: 'app-cadastro-produto',
	templateUrl: './cadastro-produto.component.html',
	styleUrls: ['./cadastro-produto.component.css']
})
export class CadastroProdutoComponent implements OnInit {

	produto: ProdutoModel = new ProdutoModel();

	form: FormGroup;

	imagemProduto: File = null;

	erro: string;

	public noWhitespaceValidator(control: FormGroup) {
		const isWhitespace = (control.value || '').trim().length === 0;
		const isValid = !isWhitespace;
		return isValid ? null : { 'whitespace': true };
	}

	constructor(private formBuilder: FormBuilder, private produtoService: ProdutoService, private http: HttpClient, private router: Router) {
		this.form = this.formBuilder.group({
			descricao: ['', [Validators.minLength(10), Validators.maxLength(45), this.noWhitespaceValidator]],
			valor: ['', [Validators.required, Validators.min(1)]],
			urlImagem: []
		});
	}

	ngOnInit() {
	}

	processFile(event : any) {
		this.imagemProduto = event.target.files[0];
		console.log(this.imagemProduto);
	}

	cadastrar() {
		if (this.form.invalid) {
			console.log('form not valid');
			return;
		}

		//this.produto.Name = this.form.get('nome').value;
		this.produto.Description = this.form.get('descricao').value;
		this.produto.Value = this.form.get('valor').value;
		this.produto.Image = this.form.get('urlImagem').value;

		console.log(this.produto);
		this.produtoService.novoProduto(this.produto).subscribe(produto => {
			this.produto = new ProdutoModel();
			this.router.navigate(['produtos']);
			console.log(produto);
		}, err => {
			this.erro = err.error.Message;
			
		})
	}

	home(){
		this.router.navigate(['clientes']);
	}

}

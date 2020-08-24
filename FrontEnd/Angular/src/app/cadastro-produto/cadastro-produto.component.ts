import { Component, OnInit } from '@angular/core';
import { ProdutoModel } from '../produtos/produto.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProdutoService } from '../produto.service';
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

  constructor(private formBuilder: FormBuilder, private produtoService: ProdutoService, private http: HttpClient, private router: Router) {
	this.form = this.formBuilder.group({
		nome: ['', Validators.required,],
		descricao: ['', Validators.required],
		valor: ['', Validators.required],

	});
   }

  ngOnInit() {
  }

  cadastrar() {
		if (this.form.invalid) {
			return;
		}

		this.produto.Name = this.form.get('nome').value;
		this.produto.Description = this.form.get('descricao').value;
		this.produto.Value = this.form.get('valor').value;

		console.log(this.produto);
		this.produtoService.novoProduto(this.produto).subscribe(produto => {
			this.produto = new ProdutoModel();
			this.router.navigate(['produtos']);
		}, err => {
			console.log('Erro ao cadastrar produto', err);
		})
	}

}

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroMensagemComponent } from './cadastro-mensagem.component';

describe('CadastroMensagemComponent', () => {
  let component: CadastroMensagemComponent;
  let fixture: ComponentFixture<CadastroMensagemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadastroMensagemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastroMensagemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdutosPedidosComponent } from './produtos-pedidos.component';

describe('ProdutosPedidosComponent', () => {
  let component: ProdutosPedidosComponent;
  let fixture: ComponentFixture<ProdutosPedidosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProdutosPedidosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProdutosPedidosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/Evento.service';
import { Evento } from '../_models/Evento';
import { BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
// Date picker
import { defineLocale, BsLocaleService , ptBrLocale } from 'ngx-bootstrap';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})

export class EventosComponent implements OnInit {

  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  acao: string;

  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  // tslint:disable-next-line:variable-name
  _filtroLista = '';

  constructor(
    private eventoService: EventoService
    // tslint:disable-next-line:align
    // , private modalService: BsModalService
    , private fb: FormBuilder
    , private localeService: BsLocaleService
    , private toastr: ToastrService
    ) {
      this.localeService.use('pt-br');
    }

  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  openModal(template: any) {
    // limpar
    this.registerForm.reset();
    template.show();
  }

  novoEvento(template: any) {
    this.acao  = 'post';
    this.openModal(template);
  }

  editarEvento(evento: Evento, template: any) {
    this.acao = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }
  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      imagemURL: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {

      if (this.acao === 'post') {
        this.evento = Object.assign({}, this.registerForm.value);
        this.eventoService.postEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );
      } else {
        this.evento = Object.assign({id : this.evento.id}, this.registerForm.value);
        this.eventoService.putEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );
      }
    }
  }

  deletarEvento(evento: Evento) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        this.toastr.success('Hello world!', 'Toastr fun!');
        this.getEventos();
      }, error => {
        console.log(error);
      }
    );
  }

  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      (_eventos: Evento[]) => {
       this.eventos = _eventos;
       this.eventosFiltrados = this.eventos;
       console.log(_eventos);
      }, error => {
        console.log(error);
      }
    );
  }
}

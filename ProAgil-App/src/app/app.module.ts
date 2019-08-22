import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BsDropdownModule, ModalModule, TooltipModule, BsDatepickerModule } from 'ngx-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ToastrModule } from 'ngx-toastr';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { EventoService } from './_services/Evento.service';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { EventosComponent } from './eventos/eventos.component';

import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipePipe
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      ModalModule.forRoot(),
      TooltipModule.forRoot(),
      ToastrModule.forRoot(),
      ReactiveFormsModule,
      BrowserAnimationsModule
   ],
   providers: [
      EventoService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

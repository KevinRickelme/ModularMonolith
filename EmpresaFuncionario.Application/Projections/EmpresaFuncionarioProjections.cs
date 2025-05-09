using Common.Application.DTOs;
using EmpresasFuncionarios.Application.Abstractions;
using EmpresasFuncionarios.Application.Services;
using EmpresasFuncionarios.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Projections
{
    public class EmpresaFuncionarioProjection(IEmpresaFuncionarioReadModelService empresaFuncionarioReadModelService, IEmpresaFuncionarioEventService empresaFuncionarioEventService) :
    INotificationHandler<FuncionarioAdmitidoEvent>,
    INotificationHandler<CargoAlteradoEvent>,
    INotificationHandler<DepartamentoAlteradoEvent>
    {
        private readonly IEmpresaFuncionarioReadModelService _empresaFuncionarioReadModelService = empresaFuncionarioReadModelService;
        private readonly IEmpresaFuncionarioEventService _empresaFuncionarioEventService = empresaFuncionarioEventService;
        public async Task Handle(FuncionarioAdmitidoEvent ev, CancellationToken ct)
        {
            var readModel = new EmpresaFuncionarioDTO
            (
                Guid.NewGuid(),
                 ev.StreamId,
                 ev.FuncionarioId,
                 ev.EmpresaId,
                 ev.DataAdmissao,
                 ev.Cargo,
                 ev.Departamento
            );

            await _empresaFuncionarioReadModelService.AddAsync(readModel, ct); // INSERT
        }

        public async Task Handle(CargoAlteradoEvent ev, CancellationToken cancellationToken)
        {
            var model = await _empresaFuncionarioReadModelService.GetByStreamIdAsync(ev.StreamId, cancellationToken);
            if (model != null)
            {
                EmpresaFuncionarioDTO readModel = ConstruirRecord(model, ev.NovoCargo, TipoAlteracao.Cargo);
                await _empresaFuncionarioReadModelService.UpdateAsync(readModel, cancellationToken); // UPDATE
            }
        }

        public async Task Handle(DepartamentoAlteradoEvent ev, CancellationToken cancellationToken)
        {
            var model = await _empresaFuncionarioReadModelService.GetByStreamIdAsync(ev.StreamId, cancellationToken);
            if (model != null)
            {
                EmpresaFuncionarioDTO readModel = ConstruirRecord(model, ev.NovoDepartamento, TipoAlteracao.Cargo);
                await _empresaFuncionarioReadModelService.UpdateAsync(readModel, cancellationToken); // UPDATE
            }
        }

        private static EmpresaFuncionarioDTO ConstruirRecord(EmpresaFuncionarioDTO model, string valor, TipoAlteracao tipoAlteracao)
        {
            switch (tipoAlteracao)
            {
                case TipoAlteracao.Cargo:
                    return new EmpresaFuncionarioDTO
                    (
                        model.Id,
                        model.StreamId,
                        model.FuncionarioId,
                        model.EmpresaId,
                        model.DataAdmissao,
                        valor,
                        model.Departamento
                    );
                case TipoAlteracao.Departamento:
                    return new EmpresaFuncionarioDTO
                    (
                        model.Id,
                        model.StreamId,
                        model.FuncionarioId,
                        model.EmpresaId,
                        model.DataAdmissao,
                        model.Cargo,
                        valor
                    );
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipoAlteracao), tipoAlteracao, null);
            }
        }


        protected enum TipoAlteracao
        {
            Cargo,
            Departamento
        }
    }

}

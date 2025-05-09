 using EmpresasFuncionarios.Domain.Events;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Domain.Entities
{
    public sealed class EmpresaFuncionario
    {
        public Guid Id { get; private set; }
        public Guid StreamId { get; private set; }
        public Guid FuncionarioId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public DateTime DataAdmissao { get; private set; }
        public string Cargo { get; private set; }
        public string Departamento { get; private set; }


        private readonly List<Event> _eventos = new();

        public EmpresaFuncionario()
        {
            Cargo = "";
            Departamento = "";
        }
        public EmpresaFuncionario(List<Event> eventos)
        {
            Cargo = "";
            Departamento = "";
            foreach (var ev in eventos.OrderBy(e => e.Timestamp))
                Apply(ev);
        }
        public void Admitir(Guid empresaFuncionarioId, Guid empresaId, Guid funcionarioId, DateTime dataAdmissao, string cargo, string departamento)
        {
            var @event = new FuncionarioAdmitidoEvent(empresaFuncionarioId, funcionarioId, empresaId, dataAdmissao, cargo, departamento);

            Apply(@event);
        }

        public void AlterarCargo(string novoCargo)
        {
            var @event = new CargoAlteradoEvent(StreamId, novoCargo);
            Apply(@event);
        }

        public void AlterarDepartamento(string novoDepartamento)
        {
            var @event = new DepartamentoAlteradoEvent(StreamId, novoDepartamento);
            Apply(@event);
        }


        public void Apply(Event @event)
        {

            switch (@event)
            {
                case FuncionarioAdmitidoEvent e:
                    Id = e.EmpresaFuncionarioId;
                    FuncionarioId = e.FuncionarioId;
                    EmpresaId = e.EmpresaId;
                    DataAdmissao = e.DataAdmissao;
                    Cargo = e.Cargo;
                    Departamento = e.Departamento;
                    break;
                case CargoAlteradoEvent e:
                    Cargo = e.NovoCargo;
                    break;
                case DepartamentoAlteradoEvent e:
                    Departamento = e.NovoDepartamento;
                    break;
                default:
                    throw new ArgumentException("Evento desconhecido.");
            }

            _eventos.Add(@event);
        }

        public static EmpresaFuncionario ReplayEvents(List<Event> eventos)
        {
            var empresaFuncionario = new EmpresaFuncionario(eventos);
            return empresaFuncionario;

        }
    }
}

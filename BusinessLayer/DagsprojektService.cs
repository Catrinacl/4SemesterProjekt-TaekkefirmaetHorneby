
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Model;
using DataAccess.Repositories;
using DTO;

namespace BusinessLogic
{
    public class DagsprojektService
    {
        private readonly DagsProjektRepository _repository = new();
        private readonly KundeRepository _kundeRepository = new();
        private readonly ArbejdsopgaveRepository _arbejdsopgaveRepository = new();
        private readonly MedarbejderRepository _medarbejderRepository = new();
        private readonly ProjektBeregner _projektBeregner = new();

        public List<OpretDagsprojektDTO> GetAlleDagsProjekter()
        {
            var dagsprojekter = _repository.GetAlleDagsProjekter();

            return dagsprojekter.Select(p => new OpretDagsprojektDTO
            {
                ProjektID = p.ProjektID,
                KundeID = p.Kundenummer,
                Adress = p.Adress,
                Status = p.Status,
                ExpectedDurationHours = p.ExpectedDurationHours,
                RoofSqrM = p.RoofSqrM,
                Startdate = p.Startdate,
                Endate = p.Endate,

                MedarbejderIds = p.Medarbejdere
                    .Select(m => m.ID)
                    .ToList(),

                Arbejdsopgaver = p.Arbejdsopgaver
                    .Select(a => new ValgtArbejdsopgaveDTO
                    {
                        ArbejdsopgaveID = a.ArbejdsopgaveID,
                        Maengde = a.Maengde,
                        ManualTimeHours = a.ManualTimeHours
                    })
                    .ToList()
            }).ToList();
        }

        public void OpretNytDagsProjekt(
            OpretDagsprojektDTO nytProjektFraUI)
        {
            KontrollerKunde(nytProjektFraUI.KundeID);

            var medarbejdere = HentValgteMedarbejdere(
                nytProjektFraUI.MedarbejderIds);

            var beregning = BeregnProjekt(
                nytProjektFraUI.Arbejdsopgaver,
                medarbejdere,
                nytProjektFraUI.Startdate);

            var dagsprojekt = new DagsProjekt
            {
                Kundenummer = nytProjektFraUI.KundeID,
                Adress = nytProjektFraUI.Adress,
                Status = nytProjektFraUI.Status,
                RoofSqrM = nytProjektFraUI.RoofSqrM,
                Startdate = nytProjektFraUI.Startdate.Date,
                Endate = beregning.Slutdato,
                ExpectedDurationHours = beregning.ForventetVarighedTimer,

                Medarbejdere = medarbejdere,
                Arbejdsopgaver = beregning.Arbejdsopgaver
            };

            _repository.GemDagsProjekt(dagsprojekt);
        }

        public void OpdaterDagsProjekt(
            OpretDagsprojektDTO projektFraUI)
        {
            KontrollerKunde(projektFraUI.KundeID);

            var eksisterendeProjekt =
                _repository.GetDagsProjekt(projektFraUI.ProjektID);

            if (eksisterendeProjekt == null)
                return;

            var medarbejdere = HentValgteMedarbejdere(
                projektFraUI.MedarbejderIds);

            var beregning = BeregnProjekt(
                projektFraUI.Arbejdsopgaver,
                medarbejdere,
                projektFraUI.Startdate);

            eksisterendeProjekt.Kundenummer = projektFraUI.KundeID;
            eksisterendeProjekt.Adress = projektFraUI.Adress;
            eksisterendeProjekt.Status = projektFraUI.Status;
            eksisterendeProjekt.RoofSqrM = projektFraUI.RoofSqrM;
            eksisterendeProjekt.Startdate = projektFraUI.Startdate.Date;
            eksisterendeProjekt.Endate = beregning.Slutdato;
            eksisterendeProjekt.ExpectedDurationHours =
                beregning.ForventetVarighedTimer;

            eksisterendeProjekt.Medarbejdere = medarbejdere;
            eksisterendeProjekt.Arbejdsopgaver =
                beregning.Arbejdsopgaver;

            _repository.OpdaterDagsProjekt(eksisterendeProjekt);
        }

        private ProjektBeregningResultat BeregnProjekt(
            List<ValgtArbejdsopgaveDTO> valgteOpgaver,
            List<Medarbejder> medarbejdere,
            DateTime startdato)
        {
            var arbejdsopgaver = valgteOpgaver
                .Select(a => _arbejdsopgaveRepository
                    .GetArbejdsopgave(a.ArbejdsopgaveID))
                .OfType<Arbejdsopgave>()
                .ToList();

            if (arbejdsopgaver.Count != valgteOpgaver.Count)
            {
                throw new InvalidOperationException(
                    "En eller flere valgte arbejdsopgaver blev ikke fundet.");
            }

            return _projektBeregner.Beregn(
                valgteOpgaver,
                arbejdsopgaver,
                medarbejdere,
                startdato);
        }

        private List<Medarbejder> HentValgteMedarbejdere(
            List<int> medarbejderIds)
        {
            var unikkeIds = medarbejderIds.Distinct().ToList();

            var medarbejdere = _medarbejderRepository
                .GetAlleMedarbejdere()
                .Where(m => unikkeIds.Contains(m.ID))
                .ToList();

            if (medarbejdere.Count != unikkeIds.Count)
            {
                throw new InvalidOperationException(
                    "En eller flere valgte medarbejdere blev ikke fundet.");
            }

            return medarbejdere;
        }

        private void KontrollerKunde(int kundenummer)
        {
            bool kundeFindes = _kundeRepository
                .GetAlleKunder()
                .Any(k => k.Kundenummer == kundenummer);

            if (!kundeFindes)
            {
                throw new InvalidOperationException(
                    $"Kunden med nummer {kundenummer} findes ikke.");
            }
        }

        public void SletDagsProjekt(int id)
        {
            _repository.SletDagsProjekt(id);
        }
    }
}

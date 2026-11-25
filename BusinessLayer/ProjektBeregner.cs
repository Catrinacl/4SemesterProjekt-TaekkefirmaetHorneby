
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Model;
using DTO;

namespace BusinessLogic
{
    public class ProjektBeregningResultat
    {
        public List<ProjektArbejdsopgave> Arbejdsopgaver { get; set; } = new();
        public double ForventetVarighedTimer { get; set; }
        public DateTime Slutdato { get; set; }
    }

    public class ProjektBeregner
    {
        private const double TimerPrArbejdsdag = 7.5;

        public ProjektBeregningResultat Beregn(
            IEnumerable<ValgtArbejdsopgaveDTO> valgteOpgaver,
            IEnumerable<Arbejdsopgave> alleOpgaver,
            IEnumerable<Medarbejder> medarbejdere,
            DateTime startdato)
        {
            var opgaveListe = alleOpgaver.ToDictionary(
                o => o.ArbejdsopgaveID);

            var medarbejderListe = medarbejdere.ToList();

            if (medarbejderListe.Count == 0)
                throw new InvalidOperationException(
                    "Vælg mindst én medarbejder.");

            var resultat = new ProjektBeregningResultat();

            foreach (var valgt in valgteOpgaver)
            {
                if (!opgaveListe.TryGetValue(
                    valgt.ArbejdsopgaveID, out var opgave))
                {
                    throw new InvalidOperationException(
                        $"Arbejdsopgaven med ID {valgt.ArbejdsopgaveID} findes ikke.");
                }

                double timer;

                if (opgave.RequiresManualTime)
                {
                    if (valgt.ManualTimeHours == null ||
                        !double.IsFinite(valgt.ManualTimeHours.Value) ||
                        valgt.ManualTimeHours.Value <= 0)
                    {
                        throw new InvalidOperationException(
                            $"Indtast et gyldigt antal timer for {opgave.Name}.");
                    }

                    timer = valgt.ManualTimeHours.Value;
                }
                else
                {
                    if (!double.IsFinite(valgt.Maengde) ||
                        valgt.Maengde <= 0)
                    {
                        throw new InvalidOperationException(
                            $"Indtast en gyldig mængde for {opgave.Name}.");
                    }

                    double produktionPrDag = medarbejderListe.Sum(m =>
                        m.Lærling
                            ? opgave.LaerlingProductionPerDay
                            : opgave.SvendProductionPerDay);

                    if (produktionPrDag <= 0)
                    {
                        throw new InvalidOperationException(
                            $"Der mangler en gyldig produktionssats for {opgave.Name}.");
                    }

                    timer = valgt.Maengde / produktionPrDag
                            * TimerPrArbejdsdag;
                }

                resultat.Arbejdsopgaver.Add(new ProjektArbejdsopgave
                {
                    ArbejdsopgaveID = opgave.ArbejdsopgaveID,
                    Maengde = valgt.Maengde,
                    ManualTimeHours = valgt.ManualTimeHours,
                    ExpectedDurationHours = timer,
                    Status = "Ikke påbegyndt",
                    Priority = 0
                });

                resultat.ForventetVarighedTimer += timer;
            }

            if (resultat.Arbejdsopgaver.Count == 0)
            {
                throw new InvalidOperationException(
                    "Vælg mindst én arbejdsopgave.");
            }

            resultat.Slutdato = BeregnSlutdato(
                startdato,
                resultat.ForventetVarighedTimer);

            return resultat;
        }

        private DateTime BeregnSlutdato(
            DateTime startdato,
            double forventetVarighedTimer)
        {
            DateTime dato = startdato.Date;

            // Flyt startdatoen frem til en hverdag, hvis den er i weekenden.
            while (dato.DayOfWeek == DayOfWeek.Saturday ||
                   dato.DayOfWeek == DayOfWeek.Sunday)
            {
                dato = dato.AddDays(1);
            }

            int antalArbejdsdage = Math.Max(
                1,
                (int)Math.Ceiling(
                    forventetVarighedTimer / TimerPrArbejdsdag));

            // Startdatoen tæller som første arbejdsdag.
            for (int dag = 1; dag < antalArbejdsdage; dag++)
            {
                dato = dato.AddDays(1);

                while (dato.DayOfWeek == DayOfWeek.Saturday ||
                       dato.DayOfWeek == DayOfWeek.Sunday)
                {
                    dato = dato.AddDays(1);
                }
            }

            return dato;
        }
    }
}

public class Tournament2   
{

    public void removeLastRound() throws Exception {
        for (/* [UNSUPPORTED] 'var' as type is unsupported "var" */ p : getParticipants())
            p.RemoveLastResult();
        for (/* [UNSUPPORTED] 'var' as type is unsupported "var" */ p : getPairings())
        {
            p.Player1Score = 0;
            p.Player2Score = 0;
            if (p.Player1.Freeticket || (p.Player1.WonFreeticket && getFirstRound()))
                p.ResultEdited = true;
            else
                p.ResultEdited = false; 
        }
    }

    public void getResults(List<Pairing> results, boolean update) throws Exception {
        Result r;
        boolean winner = new boolean();
        WinnerLastRound = new List<Player>();
        if (!update)
        {
            for (Object __dummyForeachVar13 : results)
            {
                Pairing pairing = (Pairing)__dummyForeachVar13;
                r = new Result(pairing.getPlayer1Score(),pairing.getPlayer2Score(),pairing.getPlayer2(),getFirstRound(),getMaxSquadPoints());
                winner = pairing.getPlayer1().AddResult(r);
                if (winner)
                    WinnerLastRound.Add(pairing.getPlayer1());
                 
                if (pairing.getPlayer2() != WonFreeTicket && pairing.getPlayer2() != FreeTicket)
                {
                    r = new Result(pairing.getPlayer2Score(),pairing.getPlayer1Score(),pairing.getPlayer1(),getFirstRound(),getMaxSquadPoints());
                    winner = pairing.getPlayer2().AddResult(r);
                    if (winner)
                        WinnerLastRound.Add(pairing.getPlayer2());
                     
                }
                 
            }
            setFirstRound(false);
        }
        else
        {
            for (int i = 0;i < results.Count;i++)
            {
                if (results[i].ResultEdited)
                {
                    r = new Result(results[i].Player1Score, results[i].Player2Score, results[i].Player2, getFirstRound(), getMaxSquadPoints());
                    results[i].Player1.Update(r, getDisplayedRound());
                    r = new Result(results[i].Player2Score, results[i].Player1Score, results[i].Player1, getFirstRound(), getMaxSquadPoints());
                    results[i].Player2.Update(r, getDisplayedRound());
                }
                 
            }
        } 
        for (Object __dummyForeachVar14 : getParticipants())
        {
            Player player = (Player)__dummyForeachVar14;
            player.sumEnemiesStrength();
        }
        for (int i = 0;i < results.Count;i++)
            results[i].ResultEdited = false;
        if (update)
            getRounds()[getDisplayedRound() - 1].Pairings = results;
        else
            getRounds()[getRounds().Count - 1].Pairings = results; 
        setPrePaired(new List<Pairing>());
    }

    public Player getStrongestUnplayedEnemy(Player player) throws Exception {
        for (int i = getParticipants().Count - 1;i >= 0;i--)
        {
            if (!player.HasPlayedVS(getParticipants()[i]))
                return getParticipants()[i];
             
        }
        return null;
    }


    public void removePlayer(Player player) throws Exception {
        getParticipants().Remove(player);
    }

    public void calculateWonFreeticket() throws Exception {
        if (!getWonFreeticketsCalculated())
        {
            for (/* [UNSUPPORTED] 'var' as type is unsupported "var" */ player : (getParticipants()))
            {
                if (player.WonFreeticket)
                {
                    player.AddLastEnemy(GetStrongestUnplayedEnemy(player));
                }
                 
            }
        }
         
        setWonFreeticketsCalculated(true);
    }

    public void disqualifyPlayer(Player player) throws Exception {
        for (int i = 0;i < player.getEnemies().Count;i++)
        {
            Player enemy = player.getEnemies()[i];
            player.update(new Result(0,getMaxSquadPoints(),enemy,false,getMaxSquadPoints()),i + 1);
            enemy.update(new Result(getMaxSquadPoints(),0,player,false,getMaxSquadPoints()),i + 1);
        }
        for (int i = 0;i < player.getEnemies().Count;i++)
        {
            player.getEnemies()[i].SumEnemiesStrength();
        }
        player.sumEnemiesStrength();
        player.disqualify();
    }

}
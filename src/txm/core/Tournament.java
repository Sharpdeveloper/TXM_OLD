/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package txm.core;

import java.util.ArrayList;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;
import java.util.Random;

/**
 *
 * @author Sharpdeveloper
 */
public class Tournament { 
    //name
    private String name;
    public String getName(){
        return (name == null ? name = "" : name);
    }
    public void setName(String newName){
        name = newName;
    }
    
    //maxPoints
    private int maxPoints = 0;
    public int getMaxPoints(){
        return maxPoints;
    }
    public void setMaxPoints(int newMaxPoints){
        maxPoints = newMaxPoints;
    }
    //cut
    private int cut = 0;
    public int getCut(){
        return cut;
    }
    public void setCut(int newCut){
        cut = newCut;
    }
    //T3ID
    private int T3ID;
    public int getT3ID(){
        return T3ID;
    }
    //capacity
    private int capacity = 0;
    public int getCapacity(){
        return capacity;
    }
    //rounds
    private int currentRound = 0;
    public int getCurrentRound(){
        return currentRound;
    }
    //roundsToPlay
    private int roundsToPlay = 0;
    public int getRoundsToPlay(){
        return roundsToPlay;
    }
    public void setRoundsToPlay(int newRoundsToPlay){
        roundsToPlay = newRoundsToPlay;
    }
    //cutRoundsToPlay
    private int cutRoundsToPlay = 0;
    private int getCutRoundsToPlay(){
        if(cutRoundsToPlay != 0){
            return cutRoundsToPlay;
        }
        else if(cut > 0) {
            int i = 0;
            int temp = cut;
            while(true){
                i++;
                temp /= 2;
                if(temp <= 1){
                    return roundsToPlay = i;
                }
            }
        }
        else{
            return cutRoundsToPlay = -1;
        }
    }
    
    //cutStarted
    private boolean cutStarted = false;
    public boolean isCutStarted(){
        return cutStarted;
    }
    //teamProtection
    private boolean teamProtection = false;
    public boolean hasTeamProtection(){
        return teamProtection;
    }
    public void setTeamProtection(boolean hasTeamProtection){
        teamProtection = hasTeamProtection;
    }
    
    //participants 
    private List<Player> participants;
    public List<Player> getParticipants(){
        return (participants == null ? participants = new ArrayList<>() : participants);
    }
    //prePaired
    private List<Pairing> prePaired;
    public List<Pairing> getPrePaired(){
        return (prePaired == null ? prePaired = new ArrayList<>() : prePaired);     
    }
    //pairings
    private List<Pairing> pairings;
    public List<Pairing> getPairings(){
        return (pairings == null ? pairings = new ArrayList<>() : pairings);     
    }
    public void setPairings(List<Pairing> newPairings){
        pairings = newPairings;
    }
    //pointGroups
    private List<List<Player>> pointGroups;
    //lastWinner (to save how is still in cut)
    private List<Player> lastWinner;
    
    //Constructors
    public Tournament(String _name, int _T3ID, int _roundsToPlay, List<Player> _participants, int _maxPoints, int _cut, boolean _isCutStarted, List<Pairing> _pairings){
        setName(_name);
        T3ID = _T3ID;
        participants = _participants;
        setPairings(_pairings);
        setMaxPoints(_maxPoints);
        cutStarted = _isCutStarted;
        roundsToPlay = _roundsToPlay;
    }
    
    public Tournament(String _name, int _T3ID, int _maxPoints){
        this(_name, 0, _T3ID, null, _maxPoints, 0, false, null);
    }
    
    public Tournament(String _name, int _maxPoints){
        this(_name, 0, _maxPoints);
    }
   
    //public methods
    //returns the player with the ID
    public Player getPlayerByID(int ID)
    {
        for(Player p: participants){
            if(p.getID() == ID){
                return p;
            }
        }
        return null;
    }
    
    //add a new Player
    public void addPlayer(Player player){
        getParticipants().add(player); 
    }
    
    //sorts the list with players
    public void sort(){
        throw new NoSuchMethodError();
        /*setDeafetedValues();
        List<Player> t = getParticipants().OrderByDescending(/* [UNSUPPORTED] to translate lambda expressions we need an explicit delegate type, try adding a cast "(x) => {
            return x.DropPos;
        }" ).ThenByDescending(/* [UNSUPPORTED] to translate lambda expressions we need an explicit delegate type, try adding a cast "(x) => {
            return x.Points;
        }" /).ThenByDescending(/* [UNSUPPORTED] to translate lambda expressions we need an explicit delegate type, try adding a cast "(x) => {
            return x.DefeatedAllInGroup;
        }" /).ThenByDescending(/* [UNSUPPORTED] to translate lambda expressions we need an explicit delegate type, try adding a cast "(x) => {
            return x.MarginOfVictory;
        }" /).ThenByDescending(/* [UNSUPPORTED] to translate lambda expressions we need an explicit delegate type, try adding a cast "(x) => {
            return x.PointsOfEnemies;
        }" /).ThenBy(/* [UNSUPPORTED] to translate lambda expressions we need an explicit delegate type, try adding a cast "(x) => {
            return x.Order;
        }" /).<Player>ToList();
        setParticipants(new List<Player>());
        for (Object __dummyForeachVar1 : t)
        {
            Player p = (Player)__dummyForeachVar1;
            getParticipants().Add(p);
        }
        for (int i = 0;i < getParticipants().Count;i++)
            getParticipants()[i].Rank = i + 1;*/
    }
    
    //update Player
    public void updatePlayer(Player player){
        getParticipants().set(getIndexOfParticipant(player), player);
    }
    
    public void pairNextRound(){
        currentRound++;
        boolean start = currentRound == 1;
        boolean isCut = (currentRound == roundsToPlay + 1) && (cut >= getCutRoundsToPlay());
        pairings = new ArrayList<>();
        Pairing.resetTableNo();
        List<Player> listOfPlayer = new LinkedList<>();
        sort();
        
        //create a list of all players in which the player can be deleted, so only unpaired players are on this list
        getParticipants().stream().forEach((p) -> {
            listOfPlayer.add(p);
        });
        
        ArrayList<Integer> points = new ArrayList<>();
        
        if(start){
           //WonByes
           int freeTables = listOfPlayer.size() / 2;
           for(Player p: listOfPlayer){
               if(p.hasWonBye()){
                   Pairing _p = new Pairing(freeTables++);
                   p.setBye(1);
                   _p.setPlayer(1, p);
                   _p.setPlayer(2, Player.getWonBye());
                   getPairings().add(_p);
                   listOfPlayer.remove(p);
               }
           }
           //create list of points (just 0 at start)
           points.add(0);
        }
        else{
            //create list of points
            listOfPlayer.stream().filter((p) -> (!points.contains(p.getTournamentPoints()))).forEach((p) -> {
                points.add(p.getTournamentPoints());
            });
        }
        
        Collections.sort(points);
        //move unpaired players (in round 1 wonbyes are paired) to a list separeted by points
        pointGroups = new ArrayList<>(points.size());
        for(int i=0; i< points.size();i++){
            pointGroups.set(i, new ArrayList<>());
            for(Player p: listOfPlayer){
                if(p.getTournamentPoints() == points.get(i)){
                    pointGroups.get(i).add(p);
                    listOfPlayer.remove(p);
                }
            }
        }
        
        int temp;
        Random random = new Random();
        for(int i = 0; i < pointGroups.size(); i++){
            while(pointGroups.get(i).size() >= 2){
                Pairing p = new Pairing();
                //get the first Player randomly
                temp = random.nextInt(pointGroups.get(i).size());
                p.setPlayer(1, pointGroups.get(i).get(temp));
                pointGroups.get(i).remove(temp);
                //get the second Player randomly
                temp = random.nextInt(pointGroups.get(i).size());
                int group = -1;
                for (int j = i;j < pointGroups.size();j++){
                    if (!hasPlayedVsWholePointGroup(p.getPlayer(1),j)){
                        group = j;
                        break;
                    }
                }
                if (group != -1){
                  do{
                    temp = random.nextInt(pointGroups.get(group).size());
                  } while(p.getPlayer(1).hasPlayedVS(pointGroups.get(group).get(temp)));
                }
                else {
                  //if the player has played vs all others he randomly plays vs another player
                  group = i;
                  temp = random.nextInt(pointGroups.get(group).size());
                } 
                p.setPlayer(2, pointGroups.get(group).get(temp));
                pointGroups.get(group).remove(temp);
            }
            if (pointGroups.get(i).size() == 1){
                //if its the last group there is a bye
                if (pointGroups.size() == i + 1){
                    Pairing p = new Pairing();
                    p.setPlayer(1, pointGroups.get(i).get(0));
                    p.setPlayer(2, Player.getBye());
                    p.getPlayer(1).setBye(currentRound);
                    p.setResultEdited(true);
                    getPairings().add(p);
                }
                else{
                    pointGroups.get(i + 1).add(pointGroups.get(i).get(0)); 
                }
            }
                    
        }
        //check if last 2 played together
        temp = getPairings().size();
        if (getPairings().get(temp).getPlayer(1).hasPlayedVS(getPairings().get(temp).getPlayer(2))
            && getPairings().get(temp).getPlayer(1).getEnemies().size() < getParticipants().size()) {
            for (int i = temp - 1;i >= 0;i--){
                if (!getPairings().get(temp).getPlayer(1).hasPlayedVS(getPairings().get(i).getPlayer(1))
                        && !getPairings().get(temp).getPlayer(2).hasPlayedVS(getPairings().get(i).getPlayer(2))){
                    changePairing(temp,0,i,0);
                    break;
                }

                if (!getPairings().get(temp).getPlayer(1).hasPlayedVS(getPairings().get(i).getPlayer(2))
                        && !getPairings().get(temp).getPlayer(2).hasPlayedVS(getPairings().get(i).getPlayer(1))){
                    changePairing(temp,0,i,1);
                    break;
                }
            }
        }
        
        //check if someone has to sit on a defined table
        checkTableNo();
        //sort pairings: table nr asc
        Collections.sort(getPairings(), Pairing.byTableNo);
    }
    
    private void checkTableNo() {
        getPairings().stream().forEach((p) -> {
            if (p.getPlayer(1).getTableNo() != 0
                && p.getPlayer(1).hasByeInRound(currentRound)
                && p.getTableNo()!= p.getPlayer(1).getTableNo()) {
                if ((p.getPlayer(2).getTableNo() != 0
                    && p.getPlayer(1).getTableNo() < p.getPlayer(2).getTableNo())
                    || p.getPlayer(2).getTableNo() == 0){
                    switchTableNo(p.getPlayer(1).getTableNo(),p);
                }
                else if (p.getPlayer(2).getTableNo() != 0
                        && p.getPlayer(1).getTableNo() > p.getPlayer(2).getTableNo()
                        && p.getPlayer(2).getTableNo() != p.getTableNo()){
                    switchTableNo(p.getPlayer(2).getTableNo(),p);
                }  
            }
            else if (p.getPlayer(2).getTableNo() != 0) {
                switchTableNo(p.getPlayer(2).getTableNo(),p);
            }
        });
    }
    
    private void changePairing(int player1Game, int player1Pos, int player2Game, int player2Pos){
        if (player1Game == player2Game) {
            return ;
        }
        
        // before change:
        // Player1 vs Player3
        // Player2 vs Player4
        Player player1 = getPairings().get(player1Game).getPlayer(player1Pos == 0 ? 1 : 2);
        Player player2 = getPairings().get(player2Game).getPlayer(player2Pos == 0 ? 1 : 2);
        Player player3 = getPairings().get(player1Game).getPlayer(player1Pos == 0 ? 2 : 1);
        Player player4 = getPairings().get(player2Game).getPlayer(player2Pos == 0 ? 2 : 1);
        
        
        //after change:
        //Player1 vs Player4
        //PLayer2 vs Player3
        getPairings().get(player2Game).setPlayer((player2Pos == 0) ? 1 : 2, player1);
        getPairings().get(player1Game).setPlayer((player1Pos == 0) ? 1 : 2, player2);
        
        if(player1.hasByeInRound(currentRound)){
            player1.setBye(0);
            player2.setBye(currentRound);
        }
        else if(player2.hasByeInRound(currentRound)){
            player2.setBye(0);
            player1.setBye(currentRound);
        }
        else if(player3.hasByeInRound(currentRound)){
            player3.setBye(0);
            player4.setBye(currentRound);
        }
        else if(player4.hasByeInRound(currentRound)){
            player4.setBye(0);
            player3.setBye(currentRound);
        }            
    }            
    
    private void setDeafetedValues() {
                throw new NoSuchMethodError();
/*
        if (ListOfPlayers != null)
        {
            countGroups();
            for (/* [UNSUPPORTED] 'var' as type is unsupported "var" / pl : PointGroup)
            {
                for (int i = 0;i < pl.Count;i++)
                {
                    /* [UNSUPPORTED] 'var' as type is unsupported "var" / p = pl[i];
                    if (p.Enemies.Count < pl.Count - 1)
                        p.DefeatedAllInGroup = false;
                    else
                    {
                        boolean defeated = true;
                        for (int j = 0;j < pl.Count;j++)
                        {
                            if (j == i)
                                continue;
                             
                            /* [UNSUPPORTED] 'var' as type is unsupported "var" / enemy = pl[j];
                            if (!p.HasPlayedAndWonVS(enemy))
                                defeated = false;
                             
                            if (defeated == false)
                                break;
                             
                        }
                        p.DefeatedAllInGroup = defeated;
                    } 
                }
            }
        }*/
         
    }
    
    private int getIndexOfParticipant(Player player){
        for(int i = 0; i < getParticipants().size(); i++){
            if(getParticipants().get(i).getID() == player.getID()){
                return i;
            }
        }
        return -1;
    }
    
    private boolean hasPlayedVsWholePointGroup(Player player, int groupNr) {
        Player enemy;
        for (int i = 0;i < pointGroups.get(groupNr).size();i++)
        {
            enemy = pointGroups.get(groupNr).get(i);
            if (!player.hasPlayedVS(enemy) && !player.equals(enemy))
                return false;
             
        }
        return true;
    }

    private void switchTableNo(int tableNo, Pairing pairing) {
        Pairing pairing2 = getPairingByTableNo(tableNo);
        pairing2.setTableNo(pairing.getTableNo());
        pairing.setTableNo(tableNo);
    }
    
    private Pairing getPairingByTableNo(int tableNo){
        for(Pairing p: getPairings()){
            if(p.getTableNo() == tableNo){
                return p;
            }
        }
        return null;
    }
}



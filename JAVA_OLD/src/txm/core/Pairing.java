/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package txm.core;

import java.util.Comparator;

/**
 *
 * @author Sharpdeveloper
 */
public class Pairing {
    //staticTableNr
    private static int staticTableNr = 0;
    
    //tableNr
    private int tableNo;
    public int getTableNo(){
        return tableNo;
    }
    public void setTableNo(int newTableNo){
        tableNo = newTableNo;
    }
    //player1Score
    private int player1Score;
    public int getPlayer1Score(){
        return player1Score;
    }
    public void setPlayer1Score(int newPlayer1Score){
        player1Score = newPlayer1Score;
    }
    //player1Score
    private int player2Score;
    public int getPlayer2Score(){
        return player2Score;
    }
    public void setPlayer2Score(int newPlayer2Score){
        player2Score = newPlayer2Score;
    }
    
    //resultEdited
    private boolean resultEdited;
    public boolean isResultEdited(){
        return resultEdited;
    }
    public void setResultEdited(boolean isResultEdited){
        resultEdited = isResultEdited;
    }
    
    //players
    private Player player1;
    private Player player2;
    public Player getPlayer(int playerNr){
        if(playerNr == 1){
            return (player1 == null ? player1 = new Player("New1") : player1);
        }
        else {
            return (player2 == null ? player2 = new Player("New2") : player2);
        }
    }
    public void setPlayer(int playerNr, Player newPlayer){
        if(playerNr == 1){
            player1 = newPlayer;
        } 
        else {
            player2 = newPlayer;
        }
    }
    
    //Constructors
    public Pairing(){
        setTableNo(++staticTableNr);
        setResultEdited(false);        
    }
    
    public Pairing(int tableNr){
        setTableNo(tableNr);
        setResultEdited(false); 
    }
    
    public Pairing(Pairing p) {
        this.setTableNo(p.getTableNo());
        this.setPlayer(1, p.getPlayer(1));
        this.setPlayer(2, p.getPlayer(2));
        this.setPlayer1Score(p.getPlayer1Score());
        this.setPlayer2Score(p.getPlayer2Score());
        this.setResultEdited(p.isResultEdited());
    }
 
    
    public String getPlayersName(int playerNr){
        return (playerNr == 1 ? player1.getDisplayName() : player2.getDisplayName());
    }
    
    public static void resetTableNo(){
        staticTableNr = 0;
    }
    
    public static Comparator<Pairing> byTableNo = (Pairing left, Pairing right) -> {
        if (left.getTableNo() > right.getTableNo()) {
            return 1;
        } else {
            return -1;
        }
    };
}

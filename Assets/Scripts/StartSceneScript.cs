using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using ZXing.Client.Result;
using System.Xml.Schema;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    private ThirdwebSDK sdk;

    public GameObject ConnectedState;
    public GameObject DisconnectedState;
    public GameObject StartGameState;
    public GameObject ClaimNFTState;
    public GameObject LoadingState;

    string address;
    Contract contract;

    private List<string> tokenIds = new List<string> { "0", "1", "2", "3" };
    private HashSet<string> ownedRobots = new HashSet<string>(); // Track owned robots
    // Start is called before the first frame update
    void Start()
    {
        sdk = ThirdwebManager.Instance.SDK;

        contract = sdk.GetContract("0x746700934ddAFcCf381340a4A10Ff73Df929aE1a");

        ConnectedState.SetActive(false);
        DisconnectedState.SetActive(true);
        StartGameState.SetActive(false);
        ClaimNFTState.SetActive(false);
        LoadingState.SetActive(false);
    }

    public async void ConnectWallet()
    {
        var connection = new WalletConnection(
            provider: WalletProvider.InAppWallet,
            chainId: 1,
            authOptions: new AuthOptions( authProvider: AuthProvider.Google)
        );

        address = await sdk.Wallet.Connect(connection);

        Debug.Log("Connected wallet address: " + address);

        ConnectedState.SetActive(true);
        DisconnectedState.SetActive(false);

        WalletNFTBalance();
    }

    public void DisconnectWallet()
    {
        ConnectedState.SetActive(false);
        DisconnectedState.SetActive(true);
    }

    async public void WalletNFTBalance()
    {
        // Reset owned robots
        ownedRobots.Clear();

        bool ownsAtLeastOneRobot = false;

        foreach (string tokenId in tokenIds)
        {
            Debug.Log("Checking balance for token ID: " + tokenId);

            var balance = await contract.ERC1155.BalanceOf(address, tokenId);

            Debug.Log("Balance for token ID " + tokenId + ": " + balance.ToString());

            if (int.TryParse(balance.ToString(), out int balanceInt) && balanceInt > 0)
            {
                ownedRobots.Add(tokenId); // Track owned robots
                ownsAtLeastOneRobot = true; // User owns at least one robot
            }
        }

        // If user owns at least one robot, allow them to start the game and/or claim more
        if (ownsAtLeastOneRobot)
        {
            StartGameState.SetActive(true);
            ClaimNFTState.SetActive(ownedRobots.Count < tokenIds.Count);
        }
        else
        {
            // If user doesn't own any robots, prompt them to claim a robot
            StartGameState.SetActive(false);
            ClaimNFTState.SetActive(true);
        }
    }
    async public void ClaimFirstRobot()
    {
        ClaimNFTState.SetActive(false);
        LoadingState.SetActive(true);

        var FirstClaimResult = await contract.ERC1155.Claim("0", 1);

        // Check if the claim was successful and if the user now owns the "0" token ID
        var balance = await contract.ERC1155.BalanceOf(address, "0");

        if (balance != null && int.TryParse(balance.ToString(), out int balanceInt) && balanceInt > 0)
        {
            Debug.Log("User successfully claimed the token ID '0'.");
            LoadingState.SetActive(false);
            WalletNFTBalance();
        }
        else
        {
            Debug.Log("Failed to claim token ID '0' or user does not have it.");
            LoadingState.SetActive(false);
            ClaimNFTState.SetActive(true);
        }
    }

    async public void ClaimSecondRobot()
    {
        ClaimNFTState.SetActive(false);
        LoadingState.SetActive(true);

        var FirstClaimResult = await contract.ERC1155.Claim("1", 1);

        // Check if the claim was successful and if the user now owns the "1" token ID
        var balance = await contract.ERC1155.BalanceOf(address, "1");

        if (balance != null && int.TryParse(balance.ToString(), out int balanceInt) && balanceInt > 0)
        {
            Debug.Log("User successfully claimed the token ID '1'.");
            LoadingState.SetActive(false);
            WalletNFTBalance();
        }
        else
        {
            Debug.Log("Failed to claim token ID '1' or user does not have it.");
            LoadingState.SetActive(false);
            ClaimNFTState.SetActive(true);
        }
    }

    async public void ClaimThirdRobot()
    {
        ClaimNFTState.SetActive(false);
        LoadingState.SetActive(true);

        var FirstClaimResult = await contract.ERC1155.Claim("2", 1);

        // Check if the claim was successful and if the user now owns the "2" token ID
        var balance = await contract.ERC1155.BalanceOf(address, "2");

        if (balance != null && int.TryParse(balance.ToString(), out int balanceInt) && balanceInt > 0)
        {
            Debug.Log("User successfully claimed the token ID '2'.");
            LoadingState.SetActive(false);
            WalletNFTBalance();
        }
        else
        {
            Debug.Log("Failed to claim token ID '2' or user does not have it.");
            LoadingState.SetActive(false);
            ClaimNFTState.SetActive(true);
        }
    }
    async public void ClaimFourthRobot()
    {
        ClaimNFTState.SetActive(false);
        LoadingState.SetActive(true);

        var FirstClaimResult = await contract.ERC1155.Claim("3", 1);

        // Check if the claim was successful and if the user now owns the "3" token ID
        var balance = await contract.ERC1155.BalanceOf(address, "3");

        if (balance != null && int.TryParse(balance.ToString(), out int balanceInt) && balanceInt > 0)
        {
            Debug.Log("User successfully claimed the token ID '3'.");
            LoadingState.SetActive(false);
            WalletNFTBalance();
        }
        else
        {
            Debug.Log("Failed to claim token ID '3' or user does not have it.");
            LoadingState.SetActive(false);
            ClaimNFTState.SetActive(true);
        }
    }

    public void LoadSkin()
    {
        SceneManager.LoadScene("CharacterScene");
    }
}